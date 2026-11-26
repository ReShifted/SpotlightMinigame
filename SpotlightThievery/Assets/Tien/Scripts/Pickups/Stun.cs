using UnityEngine;

public class Stun : MonoBehaviour
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
                Vector3 knockbackDirection = (other.transform.position - transform.parent.position).normalized;
                playerRb.AddForce(knockbackDirection * knockbackForce * 3, ForceMode.Impulse);
            }
        }
    }
}
