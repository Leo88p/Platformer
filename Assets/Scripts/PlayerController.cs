using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private InputManager _inputManager;
    private Rigidbody _rb;
    public GameObject MainCamera;
    private bool isJumping = false;
    private bool isOnRope = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 rotation = _inputManager.GetRotateAction();
        transform.Rotate(0, rotation.x, 0);
        MainCamera.transform.Rotate(-rotation.y, 0, 0);
        Vector3 cameraAngles = MainCamera.transform.localEulerAngles;
        if (cameraAngles.x > 45 && cameraAngles.x < 180)
        {
            cameraAngles.x = 45;
        }
        else if (cameraAngles.x >= 180 && cameraAngles.x < 315)
        {
            cameraAngles.x = 315;
        }
        MainCamera.transform.localEulerAngles = cameraAngles;
        Vector2 moveAction = _inputManager.GetMoveAction();
        if (!isOnRope)
        {
            Vector3 moveDirection = new Vector3(moveAction.x, 0, moveAction.y);
            Vector3 velocity = transform.rotation * moveDirection;
            velocity.y = _rb.linearVelocity.y;
            _rb.linearVelocity = velocity;
        }
        else
        {
            transform.position += 0.005f * moveAction.y * Vector3.up;
        }
        if (_inputManager.GetJumpAction() > 0 && !isJumping)
        {
            _rb.AddForce(5 * transform.up, ForceMode.Impulse);
            isJumping = true;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rope") && isJumping)
        {
            isJumping = false;
            isOnRope = true;
            _rb.isKinematic = true;
            return;
        }
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.75f)
            {
                isJumping= false;
                return;
            }
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rope"))
        {
            isJumping = true;
            isOnRope = false;
            _rb.isKinematic = false;
        }
    }
}
