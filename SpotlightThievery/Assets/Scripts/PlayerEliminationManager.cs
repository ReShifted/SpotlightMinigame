using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEliminationManager : MonoBehaviour
{
<<<<<<< Updated upstream
    public List<TempBenPlayerboolcheck> players;
    public List<GameObject> spotlights;
    [SerializeField] private int claimed;
    [SerializeField] private Spotlight claimedSpotlight;
    
    private void Start()
    {
        players = new List<TempBenPlayerboolcheck>(FindObjectsByType<TempBenPlayerboolcheck>(FindObjectsSortMode.None));
        spotlights = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spotlight"));
        
=======
    public List<TempBenPlayerboolcheck> players = new List<TempBenPlayerboolcheck>();
    public List<GameObject> spotlights = new List<GameObject>();

    private SpotlightManager spotlightManager;
    private bool endingRound;

    [SerializeField] private int claimed;
    public bool AllSpotlightsClaimed = false;

    private void Start()
    {
        spotlightManager = FindAnyObjectByType<SpotlightManager>();
        RefreshLists();
>>>>>>> Stashed changes
    }

    public void RefreshLists()
    {
        players = new List<TempBenPlayerboolcheck>(
            FindObjectsByType<TempBenPlayerboolcheck>(FindObjectsSortMode.None));

        spotlights = new List<GameObject>(
            GameObject.FindGameObjectsWithTag("Spotlight"));
    }

    private void Update()
    {
<<<<<<< Updated upstream
        SpotlightCheck();
    }
    private void SpotlightCheck()
    {
        foreach (GameObject spotlight in spotlights)
        {

            claimedSpotlight = spotlight.GetComponent<Spotlight>();

        }
        if (claimed == spotlights.Count)
        {
            DestroyLosers();
=======
        if (endingRound)
        {
            return;
        }

        spotlights.RemoveAll(spotlight => spotlight == null);

        if (spotlights.Count > 0 &&
            claimed >= spotlights.Count &&
            !AllSpotlightsClaimed)
        {
            AllSpotlightsClaimed = true;
            StartCoroutine(EndRoundCoroutine());
>>>>>>> Stashed changes
        }
    }

    public void AddClaimed(int amount)
    {
        claimed += amount;
    }

    private IEnumerator EndRoundCoroutine()
    {
        endingRound = true;

        for (int i = players.Count - 1; i >= 0; i--)
        {
            TempBenPlayerboolcheck player = players[i];

            if (player == null)
            {
<<<<<<< Updated upstream
                Destroy(player.transform.gameObject);
=======
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
>>>>>>> Stashed changes
            }
        }

        foreach (GameObject spotlight in spotlights)
        {
            if (spotlight != null)
            {
                Destroy(spotlight);
            }
        }

        spotlights.Clear();
        claimed = 0;
        AllSpotlightsClaimed = false;

        yield return null;

        if (players.Count > 1 && spotlightManager != null)
        {
            spotlightManager.SpawnSpotlights(players.Count);


            yield return null;
            RefreshLists();
        }
        else if (players.Count == 1)
        {
            Debug.Log(" they won the game!");
        }

        endingRound = false;
    }
<<<<<<< Updated upstream
}

=======
}
>>>>>>> Stashed changes
