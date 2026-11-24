using System.Collections.Generic;
using UnityEngine;

public class PlayerEliminationManager : MonoBehaviour
{
    public List<TempBenPlayerboolcheck> players;
    public List<GameObject> spotlights;
    [SerializeField] private int claimed;
    [SerializeField] private Spotlight claimedSpotlight;
    private PlayerEliminationManager eleimanation;
    private void Start()
    {
        players = new List<TempBenPlayerboolcheck>(FindObjectsByType<TempBenPlayerboolcheck>(FindObjectsSortMode.None));
        spotlights = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spotlight"));
        eleimanation = FindAnyObjectByType<PlayerEliminationManager>();
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

        }
        if (claimed == spotlights.Count)
        {
            DestroyLosers();
        }
    }

    public void addCliamed(int amount)
    {
        claimed += amount;
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

