using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControllerScr : MonoBehaviour
{
    private Animator playerAnimator;
    private CharacterController controller;
    private StateMachine moveSM;
    [SerializeField] private GunSelector GunSelector;
    [SerializeField] private bool AutoReload = true;
    private bool isReloading;
    [SerializeField] private PlayerIK playerIK;

    private bool isGrounded;
    private Vector3 playerFallVelocity;
    private float gravityValue = -9.81f;

    private float speed = 2f;
    private float speedMultiplier = 1;

    private const float MinSpeedMultiplier = 1;
    private const float MaxSpeedMultiplier = 2;
    private const float MultiplierChangeStep = 0.02f;

    public DefaultState defaultState;

    public delegate void ActionWithWeapon();
    public ActionWithWeapon actionWithWeapon;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        moveSM = new StateMachine();
        defaultState = new DefaultState(this, moveSM);
        moveSM.Initialize(defaultState);
        ChangeAction();
    }

    private void Update()
    {
        moveSM.currentState.HandleInput();
        moveSM.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        moveSM.currentState.PhysicsUpdate();
    }

    private void ActionWithRange()
    {
        Reload();
        Attack();
    }

    private void ActionWithMelee()
    {
        // something
    }

    private void ChangeAction()
    {
        switch (GunSelector.ActiveWeapon.WeaponType)
        {
            case WeaponType.Melee:
                actionWithWeapon = null;
                actionWithWeapon += ActionWithMelee;
                break;
            case WeaponType.Range:
                actionWithWeapon = null;
                actionWithWeapon += ActionWithRange;
                break;
            case WeaponType.Empty:
                actionWithWeapon = null;
                break;
        }
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedMultiplier = Mathf.Clamp(speedMultiplier + MultiplierChangeStep, MinSpeedMultiplier, MaxSpeedMultiplier);
        }
        else
        {
            speedMultiplier = Mathf.Clamp(speedMultiplier - MultiplierChangeStep, MinSpeedMultiplier, MaxSpeedMultiplier);
        }

        Vector3 move = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")) * speedMultiplier;

        playerAnimator.SetFloat("Move", Mathf.Max(Mathf.Abs(move.x), Mathf.Abs(move.z)));

        controller.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {
            float distance = Vector3.Distance(transform.forward, move);
            if (distance >= 2)
            {
                Vector3 playerForward = transform.forward;
                Vector3 z = Vector3.zero;
                Vector3.OrthoNormalize(ref playerForward, ref z ,ref move);
            }
            Vector3 i = Vector3.Lerp(transform.forward, move, 0.02f);
            transform.forward = i;         
        }
    }

    public void Gravity()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && playerFallVelocity.y < 0)
        {
            playerFallVelocity.y = 0;
        }
        playerFallVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerFallVelocity * Time.deltaTime);
    }

    public void Attack()
    {
        if (Input.GetMouseButton(0) && GunSelector.ActiveWeapon.RangeScrObj != null && GunSelector.ActiveWeapon.RangeScrObj.AmmoConfig.CurrentClipAmmo > 0 && !isReloading)
        {
            GunSelector.ActiveWeapon.RangeScrObj.TryToShoot();
        }
    }

    public void Reload()
    {
        if (ShouldAutoReload() || ShouldManualReload())
        {
            GunSelector.ActiveWeapon.RangeScrObj.StartReloading();
            isReloading = true;
            playerAnimator.SetTrigger("Reload");
            playerIK.HandIKAmount = 0.25f;
            playerIK.ElbowIKAmount = 0.25f;
        }
    }

    private void EndReload()
    {
        GunSelector.ActiveWeapon.RangeScrObj.EndReload();
        playerIK.HandIKAmount = 1f;
        playerIK.ElbowIKAmount = 1f;
        isReloading = false;
    }

    private bool ShouldManualReload()
    {
        return !isReloading && Input.GetKeyUp(KeyCode.R) && GunSelector.ActiveWeapon.RangeScrObj.CanReload();
    }

    private bool ShouldAutoReload()
    {
        return !isReloading && AutoReload && GunSelector.ActiveWeapon.RangeScrObj.AmmoConfig.CurrentClipAmmo == 0 && GunSelector.ActiveWeapon.RangeScrObj.CanReload();
    }
}
