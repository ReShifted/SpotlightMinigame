using System.Collections.Generic;
using UnityEngine;

public class PlayerEliminationManager : MonoBehaviour
{
    public List<TempBenPlayerboolcheck> players;
    public List<GameObject> spotlights;
    private int claimed;
    private Spotlight claimedSpotlight;
    private void Start()
    {
        players = new List<TempBenPlayerboolcheck>(FindObjectsByType<TempBenPlayerboolcheck>(FindObjectsSortMode.None));
        spotlights = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spotlight"));
    }
    private void Update()
    {
        SpotlightCheck();
    }
    private void SpotlightCheck()
    {
        foreach (GameObject spotlight in spotlights)
        {

            claimedSpotlight = spotlight.GetComponent<Spotlight>();
            if (claimedSpotlight != null && claimedSpotlight.claimed)
            {
                claimed++;
            }
        }
        if (claimed == spotlights.Count)
        {
            DestroyLosers();
        }
    }
    private void DestroyLosers()
    {
        foreach (TempBenPlayerboolcheck player in players)
        {
            if (player != null && !player.safeForNextRound)
            {
                Destroy(player.transform.gameObject);
            }
        }
    }
}

