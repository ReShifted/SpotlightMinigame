using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerEliminationManager : MonoBehaviour
{
    public List<TempBenPlayerboolcheck> players;
    public List<GameObject> spotlights;
    private SpotlightManager spotlightManager;
    [SerializeField] private int claimed;
    [SerializeField] private Spotlight claimedSpotlight;
    public bool AllSpotlightsClaimed = false;
    
    private void Start()
    {
        players = new List<TempBenPlayerboolcheck>(FindObjectsByType<TempBenPlayerboolcheck>(FindObjectsSortMode.None));
        spotlights = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spotlight"));
        spotlightManager = FindAnyObjectByType<SpotlightManager>();
    }
    private void Update()
    {
        SpotlightCheck();
        if(claimed == spotlights.Count)
        {
            AllSpotlightsClaimed = true;
        }
    }
    private void SpotlightCheck()
    {
        foreach (GameObject spotlight in spotlights)
        {

            claimedSpotlight = spotlight.GetComponent<Spotlight>();

        }
        if (AllSpotlightsClaimed)
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
                players.Remove(player);
                foreach(GameObject spotlight in spotlights)
                {
                    if(spotlight != null)
                    {
                        Destroy(spotlight.transform.gameObject);
                       
                    }
                }
                claimed = 0;
                spotlightManager.Reset();
            }
            
        }
    }

}

