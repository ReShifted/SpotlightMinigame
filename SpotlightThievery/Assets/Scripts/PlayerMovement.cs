using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public InputActionReference moveAction;
    private Rigidbody rb;

    public bool isStunned = false;
    public float stunDuration = 2f;
    private float stunTimer = 0f;
    private float stunImmuneDuration = 3f;
    private float stunImmuneTimer = 0f;

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
        if (isStunned)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            stunTimer -= Time.fixedDeltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false;
                stunImmuneTimer = stunImmuneDuration;
            }
            Debug.Log("Player is stunned. Stun timer: " + stunTimer);
            return;
        }
        if (stunImmuneTimer > 0f)
        {
            stunImmuneTimer -= Time.fixedDeltaTime;
        }
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 movement = new Vector3(input.x, 0f, input.y);
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }
        Vector3 velocity = movement * speed;
        rb.linearVelocity = new Vector3(velocity.x,rb.linearVelocity.y,velocity.z);
    }

    public void Stun()
    {
        if (!isStunned && stunImmuneTimer <= 0f)
        {
            isStunned = true;
            stunTimer = stunDuration;
        }
    }
}