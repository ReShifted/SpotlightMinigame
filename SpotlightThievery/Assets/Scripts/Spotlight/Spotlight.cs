using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spotlight : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 velocity = new(20f, 0f, 20f);
    private Vector3 currentPosition;
    private float smoothTime = 0.5f;
    private float WaitTime = 0.5f;
    private Collider floor;

    private void Awake()
    {
        if (floor == null)
        {
            GameObject floorObject = GameObject.FindWithTag("Floor");

            if (floorObject != null)
            {
                floor = floorObject.GetComponent<Collider>();
            }
        }

        if (floor == null)
        {
            Debug.LogError("No floor collider was found.", this);
        }
    }

    private void Start()
    {
        currentPosition = transform.position;
        StartCoroutine(GetNewPosition());
    }

    private void Update()
    {

    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.max.y,
            Random.Range(bounds.min.z, bounds.max.z));
    }

    private void GoToTargetPosition()
    {
        targetPosition = RandomPointInBounds(floor.bounds);
        targetPosition += Vector3.up * 8f;

        currentPosition = Vector3.SmoothDamp(
            currentPosition,
            targetPosition,
            ref velocity,
            smoothTime);
    }
    IEnumerator GetNewPosition()
    {
        if (floor == null)
        {
            return;
        }

        GoToTargetPosition();
        transform.position = currentPosition;
        yield return new WaitForSeconds(WaitTime);
        GoToTargetPosition();

    }
}
