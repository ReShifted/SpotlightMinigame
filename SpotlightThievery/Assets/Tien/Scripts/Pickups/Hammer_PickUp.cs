using Unity.VisualScripting;
using UnityEngine;

public class Hammer_PickUp : MonoBehaviour
{
    [SerializeField] private float TimeToDestroy = 5f; // Time in seconds before the pickup is destroyed
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float timer;
    [SerializeField] private float rotationSpeed = 50f; // Rotation speed in degrees per second
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the pickup object around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= TimeToDestroy)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Destroy the pickup object after it has been collected
            Destroy(gameObject);
        }
    }
}
