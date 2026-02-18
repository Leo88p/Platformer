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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 rotation = _inputManager.GetRotateAction();
        if (!isOnRope)
        {
            transform.Rotate(0, rotation.x, 0);
        }
        MainCamera.transform.Rotate(-rotation.y, 0, 0);
        Vector2 moveAction = _inputManager.GetMoveAction();
        if (isOnRope)
        {
            IsStillOnRope();
        }
        if (!isOnRope)
        {
            Vector3 moveDirection = new Vector3(moveAction.x, 0, moveAction.y);
            Vector3 velocity = transform.rotation * moveDirection;
            velocity.y = _rb.linearVelocity.y;
            _rb.linearVelocity = velocity;
        }
        else
        {
            _rb.linearVelocity += 0.5f * moveAction.y * Vector3.up;
        }
        if (_inputManager.GetJumpAction() > 0)
        {
            if (isOnGround)
            {
                _rb.AddForce(2 * transform.up, ForceMode.Impulse);
            }
            else if (isOnRope)
            {
                //_rb.AddForce(5 * transform.forward, ForceMode.Impulse);
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
                isOnRope = false;
                _rb.useGravity = true;
                return;
            }
        }
    }
    void RopeCheck(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, -transform.forward) >0.85f)
            {
                break;
            }
            else
            {
                return;
            }
        }
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
    void IsStillOnRope()
    {
        if (Physics.CapsuleCast(transform.position + 0.125f * Vector3.up, transform.position - 0.125f * Vector3.up,
            0.25f, transform.forward, out RaycastHit hit, 0.25f, LayerMask.GetMask("Rope")))
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.linearVelocity += hit.distance * transform.forward;
        }
        else
        {
            isOnRope = false;
            _rb.useGravity = true;
            _rb.linearVelocity = Vector3.zero;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rope"))
            RopeCheck(collision);
        GroundCheck(collision);
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rope"))
            RopeCheck(collision);
        GroundCheck(collision);
    }
    void OnCollisionExit(Collision collision)
    {
        isOnGround = false;
    }
}
