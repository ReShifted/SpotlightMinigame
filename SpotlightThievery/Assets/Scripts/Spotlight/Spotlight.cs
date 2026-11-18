using System.Collections;
using Unity.Hierarchy;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Spotlight : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 currentPosition;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float currentTime = 5;
    [SerializeField] private float safeTime = 3;
    private TempBenPlayerboolcheck playercheck;
    private Coroutine safeCoroutine;
    private Coroutine movementCoroutine;
    [SerializeField] private bool moveing = true;
    private Vector3 velocity;
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
        
        
        movementCoroutine = StartCoroutine(GetNewPosition());
        StartCoroutine(CountDownToStop());
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

            while (currentPosition != targetPosition)
            {
                currentPosition = Vector3.MoveTowards(
                    currentPosition,
                    targetPosition,
                    moveSpeed * Time.deltaTime);

                transform.position = currentPosition;

                yield return null;
            }
            

        }
    }
    private IEnumerator CountDownToStop()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return null;
        }
        if (currentTime <= 0)
        {
            if (movementCoroutine != null)
            {
                
                StopCoroutine(movementCoroutine);
                moveing = false;
            }
        }
    }

    private IEnumerator CountDownToSafe()
    {
        while (safeTime > 0)
        {
            safeTime -= Time.deltaTime;
            yield return null;
            
        }
        if(playercheck != null && safeTime <= 0)
        {
            playercheck.SetInsideSpotlight(true);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && moveing == false)
        {
            playercheck = other.GetComponent<TempBenPlayerboolcheck>();
            StartCoroutine(CountDownToSafe());
            return;
        }

        if (playercheck != null)
        {
            
        }
    }
}