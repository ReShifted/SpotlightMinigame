using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public InputActionReference moveAction;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 movement = new Vector3(input.x, 0f, input.y);
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }
        Vector3 velocity = movement * speed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }
}