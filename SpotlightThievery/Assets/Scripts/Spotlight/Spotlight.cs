using UnityEngine;

public class Spotlight : MonoBehaviour
{
    private Vector3 targetPosition;
    Vector3 velocity = new Vector3(0, 0, 2);
    private Vector3 currentPosition;
    private float smoothTime = 3;
    private Collider Floor; // Change from GameObject to Collider

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Floor = GetComponent<Collider>();
        transform.position = currentPosition;
    }

    // Update is called once per frame
    void Update()
    {

        GoToTargetPosition();
        
    }
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
    private void GoToTargetPosition()
    {
        targetPosition = RandomPointInBounds(Floor.bounds); // Now Floor has a 'bounds' property
        currentPosition = Vector3.SmoothDamp(currentPosition, targetPosition, ref velocity, smoothTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.inSpotlight = true;
        }
    }
}
