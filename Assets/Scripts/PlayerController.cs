using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private InputManager _inputManager;
    private Rigidbody _rb;
    public GameObject MainCamera;
    private bool isOnGround = true;
    private bool isOnRope = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _rb = GetComponent<Rigidbody>();
        EventManager.OnRopeTriggered += RopeTriggered;
        EventManager.OnRopeExit += RopeExit;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 rotation = _inputManager.GetRotateAction();
        transform.Rotate(0, rotation.x, 0);
        MainCamera.transform.Rotate(-rotation.y, 0, 0);
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
            _rb.linearVelocity = 0.5f * moveAction.y * Vector3.up;
        }
        if (_inputManager.GetJumpAction() > 0)
        {
            if (isOnGround)
            {
                _rb.AddForce(2 * transform.up, ForceMode.Impulse);
            }
            else if (isOnRope)
            {
                _rb.AddForce(5 * transform.forward, ForceMode.Impulse);
            }
        }
    }
    void GroundCheck(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.75f)
            {
                isOnGround = true;
                return;
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        GroundCheck(collision);
    }
    void OnCollisionStay(Collision collision)
    {
        GroundCheck(collision);
    }
    void OnCollisionExit(Collision collision)
    {
        isOnGround = false;
    }
    void RopeTriggered()
    {
        if (!isOnGround)
        {
            isOnRope = true;
            _rb.linearVelocity = Vector3.zero;
            _rb.useGravity = false;
        }
        else
        {
            isOnRope = false;
            _rb.useGravity = true;
        }
    }
    void RopeExit()
    {
        isOnRope = false;
        _rb.useGravity = true;
    }
}
