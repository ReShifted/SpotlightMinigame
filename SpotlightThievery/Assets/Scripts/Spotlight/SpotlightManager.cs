using System.Collections.Generic;
using UnityEngine;

public class SpotlightManager : MonoBehaviour
{
    [SerializeField] private GameObject spotlightPrefab;
    [SerializeField] private GameObject floor;

    private void Awake()
    {
        GameObject[] initialPlayers = GameObject.FindGameObjectsWithTag("Player");
        SpawnSpotlights(initialPlayers.Length);
    }

    public void SpawnSpotlights(int activePlayerCount)
    {
        if (spotlightPrefab == null || floor == null) return;

        for (int i = 1; i < activePlayerCount; i++)
        {
            Instantiate(
                spotlightPrefab,
                floor.transform.position + new Vector3(0, 8, 0),
                Quaternion.Euler(90, 0, 0)
            );
        }
    }
}
