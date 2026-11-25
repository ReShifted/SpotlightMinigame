using UnityEngine;

public class Stun : MonoBehaviour
{
    public float knockbackForce = 10f;
    private GameObject parentobject;

    private void Start()
    {
        parentobject = gameObject.transform.parent.gameObject;
        Debug.Log("Stun script is active on " + parentobject.name);
    }
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
                playerRb.AddForce(knockbackDirection * knockbackForce * 2 , ForceMode.Impulse);
            }
        }
    }
}
