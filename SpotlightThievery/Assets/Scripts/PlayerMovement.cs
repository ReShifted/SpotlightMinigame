using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public InputActionReference moveActionReference;

    private InputActionAsset inputAsset; // this player's own copy of the asset
    private InputAction moveAction;      // the Move action inside that copy
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

        inputAsset = Instantiate(moveActionReference.asset);
        moveAction = inputAsset.FindAction(moveActionReference.action.id.ToString());

        inputAsset.devices = new ReadOnlyArray<InputDevice>(new InputDevice[0]);
    }

    private void OnEnable() => moveAction.Enable();
    private void OnDisable() => moveAction.Disable();
    private void OnDestroy() => Destroy(inputAsset);

    public void AssignDevice(InputDevice device)
    {
        var devices = device != null ? new InputDevice[] { device } : new InputDevice[0];
        inputAsset.devices = new ReadOnlyArray<InputDevice>(devices);
    }

    private void FixedUpdate()
    {
        if (isStunned)
        {
            stunTimer -= Time.fixedDeltaTime;

            if (stunTimer <= 0f)
            {
                isStunned = false;
                stunImmuneTimer = stunImmuneDuration;
            }

            return;
        }

        if (stunImmuneTimer > 0f)
        {
            stunImmuneTimer -= Time.fixedDeltaTime;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(input.x, 0f, input.y);
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        Vector3 velocity = movement * speed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
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