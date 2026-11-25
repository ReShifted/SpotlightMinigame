using System.Collections.Generic;
using UnityEngine;

public class SpotlightManager : MonoBehaviour
{
    [SerializeField] private GameObject spotlightPrefab;
    [SerializeField] private GameObject floor;
<<<<<<< Updated upstream
    public List<GameObject> players;

    private void Awake()
    {

        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
=======

    private void Awake()
    {
        GameObject[] initialPlayers = GameObject.FindGameObjectsWithTag("Player");
        SpawnSpotlights(initialPlayers.Length);
    }
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
