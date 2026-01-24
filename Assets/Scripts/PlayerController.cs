using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private InputManager _inputManager;
    private Rigidbody _rb;
    private bool isJumping = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(transform.up, _inputManager.GetRotateAction());
        Vector2 moveAction = _inputManager.GetMoveAction();
        Vector3 moveDirection = new Vector3(moveAction.x, 0, moveAction.y);
        Vector3 velocity = transform.rotation * moveDirection;
        velocity.y = _rb.linearVelocity.y;
        _rb.linearVelocity = velocity;
        if (_inputManager.GetJumpAction() > 0 && !isJumping)
        {
            _rb.AddForce(5 * transform.up, ForceMode.Impulse);
            isJumping = true;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.75f)
            {
                isJumping= false;
                return;
            }
        }
    }
}
