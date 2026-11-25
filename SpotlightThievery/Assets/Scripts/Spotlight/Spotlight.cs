using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : MonoBehaviour
{
    public List<TempBenPlayerboolcheck> players = new List<TempBenPlayerboolcheck>();
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 currentPosition;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float currentTime = 5f;
    [SerializeField] private float safeStart = 3f;
    [SerializeField] private float safecurrent;

    private TempBenPlayerboolcheck playercheck;
    private PlayerEliminationManager elimination;
    private Coroutine movementCoroutine;
    private Coroutine safeCoroutine;

    [SerializeField] private bool moving = true;
    public bool claimed = false;

    private Collider floor;

    private void Awake()
    {
        GameObject floorObject = GameObject.FindWithTag("Floor");
        if (floorObject != null)
        {
            floor = floorObject.GetComponent<Collider>();
        }

        if (floor == null)
        {
            Debug.LogError("No floor collider found with tag 'Floor'.", this);
        }
    }

    private void Start()
    {
        elimination = FindAnyObjectByType<PlayerEliminationManager>();
        currentPosition = transform.position;
        safecurrent = safeStart;

        movementCoroutine = StartCoroutine(GetNewPosition());
        StartCoroutine(CountDownToStop());
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.max.y,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private IEnumerator GetNewPosition()
    {
        while (floor != null)
        {
            targetPosition = RandomPointInBounds(floor.bounds) + Vector3.up * 8f;

            while (Vector3.Distance(currentPosition, targetPosition) > 0.01f)
            {
                currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
                transform.position = currentPosition;
                yield return null;
            }
        }
    }

    private IEnumerator CountDownToStop()
    {
        while (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            yield return null;
        }

        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }
        moving = false;

        
        CheckClaimCondition();
    }

    private IEnumerator CountDownToSafe()
    {
        while (playercheck != null && safecurrent > 0f && !moving && !claimed && players.Count == 1 && playercheck.insideSpotlight)
        {
            safecurrent -= Time.deltaTime;
            yield return null;
        }

        safeCoroutine = null;

        if (playercheck != null && safecurrent <= 0f && players.Count == 1 && !claimed)
        {
            claimed = true;
            playercheck.SetInsideSpotlight(true);
            if (elimination != null)
            {
                elimination.AddClaimed(1);
            }
        }
    }

    private void CheckClaimCondition()
    {
        if (moving || claimed) return;

        if (players.Count == 1)
        {
            playercheck = players[0];
            if (safeCoroutine == null)
            {
                safecurrent = safeStart;
                safeCoroutine = StartCoroutine(CountDownToSafe());
            }
        }
        else
        {
            if (safeCoroutine != null)
            {
                StopCoroutine(safeCoroutine);
                safeCoroutine = null;
            }
            safecurrent = safeStart;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        TempBenPlayerboolcheck player = other.GetComponentInParent<TempBenPlayerboolcheck>();
        if (player == null || players.Contains(player)) return;

        players.Add(player);
        player.insideSpotlight = true;

        CheckClaimCondition();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        TempBenPlayerboolcheck player = other.GetComponentInParent<TempBenPlayerboolcheck>();
        if (player == null || !players.Remove(player)) return;

        player.insideSpotlight = false;

        if (playercheck == player)
        {
            playercheck = players.Count > 0 ? players[0] : null;
        }

        CheckClaimCondition();
    }
}
