using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Weapon", menuName = "WeaponData/Range/Weapon", order = 1)]

public class RangeSO : ScriptableObject
{
    public ImpactType ImpactType;
    public string Name;
    public RangeType Type;
    public GameObject ModelPrefab;
    public Vector3 SpawnPosition;
    public Vector3 SpawnRotation;

    public ShootConfigSO ShootConfig;
    public TrailConfigSO TrailConfig;
    public AmmoConfigSO AmmoConfig;
    public AudioConfigSO AudioConfig;
    public DamageConfigSO DamageConfig;

    private MonoBehaviour ActiveMonoBehaviour;
    private GameObject model;
    private AudioSource ShootingAudio;
    private float LastShootTime;
    private ParticleSystem ShootSystem;
    private ObjectPool<TrailRenderer> TrailPool;
    private delegate bool FireType(int i);// доделать
    FireType fireType; //

    

    public void Spawn(MonoBehaviour ActiveMonoBehaviour, Transform Parent)
    {
        this.ActiveMonoBehaviour = ActiveMonoBehaviour;
        LastShootTime = 0f;
        TrailPool = new ObjectPool<TrailRenderer>(CreateTrail);

        model = Instantiate(ModelPrefab);
        model.transform.SetParent(Parent, false);
        model.transform.localPosition = SpawnPosition;
        model.transform.localRotation = Quaternion.Euler(SpawnRotation);

        ShootSystem = model.GetComponentInChildren<ParticleSystem>();
        ShootingAudio = model.GetComponent<AudioSource>();

        if (Type == RangeType.Pistol)  //
        {
            fireType = Input.GetMouseButtonDown;
        }
        else
        {
            fireType = Input.GetMouseButton;
        }
        
    }

    public void TryToShoot()
    {
        if (Time.time > ShootConfig.FireRate + LastShootTime && AmmoConfig.CurrentClipAmmo > 0)
        {
            LastShootTime = Time.time;

            if(AmmoConfig.CurrentClipAmmo == 0)
            {
                AudioConfig.PlayOutOfAmmoClip(ShootingAudio);
                return;
            }

            ShootSystem.Play();
            AudioConfig.PlayShootClip(ShootingAudio, AmmoConfig.CurrentClipAmmo == 1);

            Vector3 shootDirection = ShootSystem.transform.forward +
                new Vector3(Random.Range(-ShootConfig.Spread.x, ShootConfig.Spread.x),
                            Random.Range(-ShootConfig.Spread.y, ShootConfig.Spread.y),
                            Random.Range(-ShootConfig.Spread.z, ShootConfig.Spread.z));
            shootDirection.Normalize();

            AmmoConfig.CurrentClipAmmo--;

            if (Physics.Raycast(ShootSystem.transform.position, shootDirection, out RaycastHit hit, float.MaxValue, ShootConfig.HitMask))
            {
                ActiveMonoBehaviour.StartCoroutine(PlayTrail(ShootSystem.transform.position, hit.point, hit));
            }
            else
            {
                ActiveMonoBehaviour.StartCoroutine(PlayTrail(ShootSystem.transform.position, ShootSystem.transform.position + (shootDirection * TrailConfig.MissDistance), new RaycastHit()));
            }
        }
    }

    public bool CanReload()
    {
        return AmmoConfig.CanReload();
    }

    public void StartReloading()
    {
        AudioConfig.PlayReloadClip(ShootingAudio);
    }

    public void EndReload()
    {
        AmmoConfig.Reload();
    }

    private IEnumerator PlayTrail(Vector3 StartPoint, Vector3 EndPoint, RaycastHit Hit)
    {
        TrailRenderer instance = TrailPool.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = StartPoint;
        yield return null;

        instance.emitting = true;

        float distance = Vector3.Distance(StartPoint, EndPoint);
        float remainingDistance = distance;
        while (remainingDistance > 0)
        {
            instance.transform.position = Vector3.Lerp(StartPoint, EndPoint, Mathf.Clamp01(1 - (remainingDistance / distance)));
            remainingDistance -= TrailConfig.SimulationSpeed * Time.deltaTime;
            yield return null;
        }

        instance.transform.position = EndPoint;

        if(Hit.collider != null)
        {
            SurfaceManager.Instance.HandleImpact(Hit.transform.gameObject, EndPoint, Hit.normal, ImpactType, 0);
            if(Hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(DamageConfig.GetDamage());
            }
        }

        yield return new WaitForSeconds(TrailConfig.Duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        TrailPool.Release(instance);
    }


    private TrailRenderer CreateTrail()
    {
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.colorGradient = TrailConfig.Color;
        trail.material = TrailConfig.Material;
        trail.widthCurve = TrailConfig.WidthCurve;
        trail.time = TrailConfig.Duration;
        trail.minVertexDistance = TrailConfig.MinVertexDistance;

        return trail;
    }
}
