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
    private Coroutine movementCoroutine;
    [SerializeField] private bool moveing = true;
    [SerializeField] private bool claimed = false;
    
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
        while (safecurrent > 0 && moveing == false && playercheck.insideSpotlight == true && claimed == false && players.Count== 1)
        {
            safecurrent -= Time.deltaTime;
            yield return null;
            
        }
        if(playercheck != null && safecurrent <= 0)
        {
            claimed = true;
            playercheck.SetInsideSpotlight(true);
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || moveing || claimed)
        {
            return;
        }

        TempBenPlayerboolcheck player = other.GetComponentInParent<TempBenPlayerboolcheck>();

        if (player == null || players.Contains(player))
        {
            return;
        }

        players.Add(player);
        playercheck = player;
        player.insideSpotlight = true;

        StartCoroutine(CountDownToSafe());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        TempBenPlayerboolcheck player = other.GetComponentInParent<TempBenPlayerboolcheck>();

        if (player == null)
        {
            return;
        }

        if (players.Remove(player))
        {
            player.insideSpotlight = false;
            StopCoroutine(CountDownToSafe());
            safecurrent = safeStart;
        }

        if (playercheck == player)
        {
            playercheck = players.Count > 0 ? players[0] : null;
        }
    }
}