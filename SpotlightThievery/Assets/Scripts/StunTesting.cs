using UnityEngine;

public class StunTesting : MonoBehaviour
{
    public float knockbackForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            Rigidbody playerRb = other.GetComponent<Rigidbody>();

            if (playerMovement != null && playerRb != null)
            {
                playerMovement.Stun();
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDirection * knockbackForce,ForceMode.Impulse);
            }
        }
    }
}