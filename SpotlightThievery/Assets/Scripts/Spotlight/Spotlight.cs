using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Spotlight : MonoBehaviour
{
    public List<TempBenPlayerboolcheck> players = new List<TempBenPlayerboolcheck>();
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 currentPosition;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float currentTime = 5;
    [SerializeField] private float safeStart = 3;
    [SerializeField] private float safecurrent;
    private TempBenPlayerboolcheck playercheck;
    private PlayerEliminationManager eleimanation;
    private Coroutine movementCoroutine;
    private Coroutine safeCoroutine;
    [SerializeField] private bool moveing = true;
    public bool claimed = false;

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
            Debug.LogError("No floor collider.", this);
        }
    }

    private void Start()
    {
        eleimanation = FindAnyObjectByType<PlayerEliminationManager>();
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
        while (playercheck != null && safecurrent > 0 && !moveing && !claimed && players.Count == 1 && playercheck.insideSpotlight)
        {
            safecurrent -= Time.deltaTime;
            yield return null;

        }
        safeCoroutine = null;
        if (playercheck != null && safecurrent <= 0 && players.Count == 1 && !claimed)
        {
            claimed = true;
            eleimanation.addCliamed(1);
            playercheck.SetInsideSpotlight(true);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || moveing || claimed)
        {
            return;
        }
        else Debug.Log("No Player in spotlight");
        TempBenPlayerboolcheck player = other.GetComponentInParent<TempBenPlayerboolcheck>();

        players.Add(player);
        player.insideSpotlight = true;
        safecurrent = safeStart;
        if (playercheck == null)
        {
            playercheck = player;
        }
        if (players.Count == 1 && safeCoroutine == null)
        {
            StartCoroutine(CountDownToSafe());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        TempBenPlayerboolcheck player = other.GetComponentInParent<TempBenPlayerboolcheck>();

        if (player == null || !players.Remove(player))
        {
            return;
        }

        player.insideSpotlight = false;

        if (playercheck == player)
        {
            playercheck = players.Count > 0 ? players[0] : null;
        }

        if (players.Count == 0)
        {
            if (safeCoroutine != null)
            {
                StopCoroutine(safeCoroutine);
                safeCoroutine = null;
            }

            safecurrent = safeStart;
            return;
        }

        if (players.Count == 1 && safeCoroutine == null && !claimed)
        {
            safeCoroutine = StartCoroutine(CountDownToSafe());
        }
    }
}