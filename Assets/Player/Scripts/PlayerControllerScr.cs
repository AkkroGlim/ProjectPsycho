using JetBrains.Annotations;
using UnityEngine;

public class PlayerControllerScr : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody playerRigid;
    private Collider playerCol;
    private StateMachine moveSM;
    [SerializeField] private GunSelector GunSelector;
    [SerializeField] private bool AutoReload = true;
    private bool isReloading;
    [SerializeField] private PlayerIK playerIK;

    private float defaultPlayerPositionX;
    public bool mayInteract { get; private set; }
    private Vector3 shelterPosition;
    private Vector3 hidingPosition;
    private Vector3 distanceToShelter;
    private Vector3 vaultPosition;

    private float speed = 70f;
    private float walkSpeed = 70f;
    private float speedMultiplier = 1;

    private float mousePositionX;
    private float newMousePositionX;

    public DefaultState defaultState;
    public HidingState hidingState;
    public TurnState turnState;
    public VaultState vaultState;

    public delegate void ActionWithWeapon();
    public ActionWithWeapon actionWithWeapon;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        playerCol = GetComponent<Collider>();
        playerAnimator = GetComponent<Animator>();
        TriggerEvent.triggerEvent.AddListener(InteractZone);
        defaultPlayerPositionX = transform.position.x;
        mayInteract = false;

        mousePositionX = 1f;
        newMousePositionX = Mathf.Sign(Input.mousePosition.x - Screen.width / 2);
        playerAnimator.SetFloat("DirectionFloat", newMousePositionX);

        moveSM = new StateMachine();
        defaultState = new DefaultState(this, moveSM);
        hidingState = new HidingState(this, moveSM);
        turnState = new TurnState(this, moveSM);
        vaultState = new VaultState(this, moveSM);

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

    public void Centring()
    {
        if (defaultPlayerPositionX != transform.position.x)
        {
            Vector3 target = transform.position;
            target.x = defaultPlayerPositionX;
            transform.position = Vector3.MoveTowards(transform.position, target, 3 * Time.deltaTime);
        }
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

    public void Move(float moveDirection, float faceDirection)
    {
        if (Input.GetKey(KeyCode.LeftShift) && moveDirection == faceDirection)
        {
            speedMultiplier = Mathf.Clamp(speedMultiplier + 0.04f, 1f, 2f);
        }
        else
        {
            speedMultiplier = Mathf.Clamp(speedMultiplier - 0.04f, 1f, 2f);
        }

        speed = walkSpeed * moveDirection * speedMultiplier;
        moveDirection = moveDirection * speedMultiplier;
        playerAnimator.SetFloat("Move", moveDirection);

        Vector3 targetVelocity = speed * transform.forward * Time.deltaTime * faceDirection;
        targetVelocity.y = playerRigid.velocity.y;
        playerRigid.velocity = targetVelocity;

        if (speed != 0)
        {
            PlayerEvent.moveEvent.Invoke(true);
        }
        else
        {
            PlayerEvent.moveEvent.Invoke(false);
        }
    }

    public void Turn(float direction)
    {
        transform.Rotate(0f, -500 * direction * Time.deltaTime, 0f);
    }

    public bool IsPlayerTurnAround()
    {
        newMousePositionX = Mathf.Sign(Input.mousePosition.x - Screen.width / 2);
        if (mousePositionX != newMousePositionX)
        {
            mousePositionX = newMousePositionX;
            playerAnimator.SetFloat("DirectionFloat", mousePositionX);
            return true;
        }
        return false;
    }

    public void ActiveTurnAnimation()
    {
        playerAnimator.SetTrigger("Turn");
    }

    public void ActiveVaultAnimation()
    {
        playerAnimator.SetTrigger("Vault");
    }

    public void Hiding()
    {
        transform.position = Vector3.MoveTowards(transform.position, hidingPosition, 3 * Time.deltaTime);
    }

    private void InteractZone(Vector3 shelterPosition, Vector3 distance, bool mayInteract)
    {
        this.shelterPosition = shelterPosition;
        distanceToShelter = distance;
        hidingPosition = shelterPosition;
        hidingPosition.y = transform.position.y;
        hidingPosition += distanceToShelter * Mathf.Sign(transform.position.z - this.shelterPosition.z);

        this.mayInteract = mayInteract;
    }

    public void Vault()
    {
        transform.position = Vector3.MoveTowards(transform.position, vaultPosition, Time.deltaTime);
    }

    public Vector3 PrepareToVault()
    {
        transform.position = hidingPosition;
        vaultPosition = hidingPosition -= 2 * distanceToShelter * Mathf.Sign(transform.position.z - shelterPosition.z);
        return vaultPosition;
    }

    public void PlayerTangibilityTogle()
    {
        playerRigid.useGravity = !playerCol.enabled;
        playerCol.enabled = !playerCol.enabled;
    }

    public bool MayVault()
    {
        if (mousePositionX == Mathf.Sign(shelterPosition.z - transform.position.z) && mayInteract)
        {
            return true;
        }
        return false;
    }

    public void RemoveSpeed()
    {
        playerRigid.velocity = Vector3.zero;
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
