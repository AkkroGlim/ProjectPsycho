using UnityEngine.InputSystem;
using UnityEngine;

public class StarterAssetsInputs : MonoBehaviour
{
    public Vector2 move;
    public bool sprint;
    public bool aiming;
    public Vector2 lookAtMouse;

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void OnAiming(InputValue value)
    {
        AimingInput(value.isPressed);
    }

    public void OnLookAtMouse(InputValue value)
    {
        LookAtMouseInput(value.Get<Vector2>());
    }



    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    public void AimingInput(bool newAimingState)
    {
        aiming = newAimingState;
    }

    public void LookAtMouseInput(Vector2 newMousePosition)
    {
        lookAtMouse = newMousePosition;
    }
}
