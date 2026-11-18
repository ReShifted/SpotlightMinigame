using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spotlight : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    private Vector3 velocity = new(20f, 0f, 20f);
    [SerializeField] private Vector3 currentPosition;
    [SerializeField] private float smoothTime = 0.5f;
    [SerializeField] private float WaitTime = 0.5f;
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

    private IEnumerator GetNewPosition()
    {
        while (true)
        {
            targetPosition = RandomPointInBounds(floor.bounds);
            targetPosition += Vector3.up * 8f;

            velocity = Vector3.zero;

            while (Vector3.Distance(currentPosition, targetPosition) > 0.05f)
            {
                currentPosition = Vector3.SmoothDamp(
                    currentPosition,
                    targetPosition,
                    ref velocity,
                    smoothTime);

                transform.position = currentPosition;

                yield return null;
            }

            transform.position = targetPosition;
            currentPosition = targetPosition;

            yield return new WaitForSeconds(WaitTime);
        }
    }
}
