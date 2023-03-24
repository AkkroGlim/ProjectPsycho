using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControllerScr : MonoBehaviour
{
    private Animator playerAnimator;
    private CharacterController controller;
    private StateMachine moveSM;
    private StarterAssetsInputs input;

    [SerializeField] private GunSelector GunSelector;
    [SerializeField] private bool AutoReload = true;
    private bool isReloading;
    [SerializeField] private PlayerIK playerIK;

    [SerializeField] private Camera cam;

    private bool isGrounded;
    private Vector3 playerFallVelocity;
    private float gravityValue = -9.81f;

    private float moveSpeed = 2f;
    private float sprintSpeed = 6f;
    private float speedChangeRate = 10f;
    private float speed;

    private float animationBlend;

    public DefaultState defaultState;

    public delegate void ActionWithWeapon();
    public ActionWithWeapon actionWithWeapon;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        input = GetComponent<StarterAssetsInputs>();

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
        float targetSpeed = input.sprint ? sprintSpeed : moveSpeed;

        if (input.move == Vector2.zero) targetSpeed = 0;

        speed = Mathf.Lerp(speed, targetSpeed, speedChangeRate * Time.deltaTime);
        speed = Mathf.Round(speed * 1000f) / 1000f;
        speed = Mathf.Clamp(speed, 0, targetSpeed);


        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, speedChangeRate * Time.deltaTime);

        if (animationBlend < 0.01f) animationBlend = 0f;

        Vector3 inputDirection = new Vector3(-input.move.y, 0f, input.move.x);      
        controller.Move(inputDirection.normalized * speed * Time.deltaTime);
        playerAnimator.SetFloat("Move", animationBlend);
    }

    public void Turn()
    {
        Vector3 inputDirection = new Vector3(-input.move.y, 0f, input.move.x);

        if (input.move != Vector2.zero)
        {
            float distance = Vector3.Distance(transform.forward, inputDirection);
            if (distance >= 2)
            {
                Vector3 playerForward = transform.forward;
                Vector3 z = Vector3.zero;
                Vector3.OrthoNormalize(ref playerForward, ref z, ref inputDirection);
            }
            Vector3 i = Vector3.Lerp(transform.forward, inputDirection, 0.02f);
            transform.forward = i;
        }
    }

    public void LookAtMouse()
    {
        Vector2 playerInScreen = cam.WorldToScreenPoint(transform.position + Vector3.up);
        Vector2 targetRotate = input.lookAtMouse - playerInScreen;
        targetRotate = targetRotate.normalized;
        transform.forward = Vector3.Lerp(transform.forward, new Vector3(-targetRotate.y, 0f, targetRotate.x), 0.02f);
        
        // Необходимо пофиксить. Прицелиться в объект возможно только при наведение на его основание курсором
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
