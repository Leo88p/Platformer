using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction rotateAction;
    private InputAction jumpAction;

    void Awake()
    {
        PlayerInput _playerInput = GetComponent<PlayerInput>();
        InputActionMap currentMap = _playerInput.currentActionMap;
        moveAction = currentMap.FindAction("Move");
        rotateAction = currentMap.FindAction("Rotate");
        jumpAction = currentMap.FindAction("Jump");
    }

    public Vector2 GetMoveAction()
    {
        return moveAction.ReadValue<Vector2>();
    }
    public Vector2 GetRotateAction()
    {
        return rotateAction.ReadValue<Vector2>();
    }
    public float GetJumpAction()
    {
        return jumpAction.ReadValue<float>();
    }
}
