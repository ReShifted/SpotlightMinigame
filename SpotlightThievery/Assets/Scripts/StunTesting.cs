using UnityEngine;

public class StunTesting : MonoBehaviour
{

    public float knockbackForce = 10f;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.Stun();
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}