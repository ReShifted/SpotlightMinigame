using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEliminationManager : MonoBehaviour
{
    public List<TempBenPlayerboolcheck> players = new List<TempBenPlayerboolcheck>();
    public List<GameObject> spotlights = new List<GameObject>();
    private SpotlightManager spotlightManager;

    [SerializeField] private int claimed;
    public bool AllSpotlightsClaimed = false;

    private void Start()
    {
        spotlightManager = FindAnyObjectByType<SpotlightManager>();
        RefreshLists();
    }

    public void RefreshLists()
    {
        players = new List<TempBenPlayerboolcheck>(FindObjectsByType<TempBenPlayerboolcheck>(FindObjectsSortMode.None));
        spotlights = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spotlight"));
    }

    private void Update()
    {

        players.RemoveAll(p => p == null);
        spotlights.RemoveAll(s => s == null);

        if (spotlights.Count > 0 && claimed >= spotlights.Count && !AllSpotlightsClaimed)
        {
            AllSpotlightsClaimed = true;
            EndRound();
        }
    }

    public void AddClaimed(int amount)
    {
        claimed += amount;
    }

    private void EndRound()
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            TempBenPlayerboolcheck player = players[i];

            if (player == null)
            {
                players.RemoveAt(i);
                continue;
            }

            if (!player.safeForNextRound)
            {
                Destroy(player.gameObject);
                players.RemoveAt(i);
            }
            else
            {
                player.safeForNextRound = false;
                player.insideSpotlight = false;
            }
        }

        foreach (GameObject spotlight in spotlights)
        {
            if (spotlight != null)
            {

                spotlight.tag = "Untagged";
                Destroy(spotlight);
            }
        }
        spotlights.Clear();

        claimed = 0;
        AllSpotlightsClaimed = false;

        if (players.Count > 1 && spotlightManager != null)
        {
            spotlightManager.SpawnSpotlights(players.Count);
            spotlights = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spotlight"));
        }
        else if (players.Count == 1)
        {
            Debug.Log($"{players[0].name} wins the game!");
            SceneManager.LoadScene("Win scene");
        }
    }
}
