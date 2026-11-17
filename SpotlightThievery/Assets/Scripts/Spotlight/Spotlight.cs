using UnityEngine;

public class Spotlight : MonoBehaviour
{
    [SerializeField] private GameObject Floor;
    private transform targetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GoToTargetPosition()
    {
        transform.position = currentPosition;
        smoothDamp()
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.inSpotlight = true;
        }
    }
}
