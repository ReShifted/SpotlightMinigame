using UnityEngine;

public class TempBenPlayerboolcheck : MonoBehaviour
{
    private PlayerEliminationManager elimination;
    public bool safeForNextRound;
    public bool insideSpotlight;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        elimination = FindAnyObjectByType<PlayerEliminationManager>();
        safeForNextRound = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(elimination.AllSpotlightsClaimed)
        {
            SetInsideSpotlight(false);
        }
    }

    public void SetInsideSpotlight(bool value)
    {
        insideSpotlight = value;
        safeForNextRound = value;
    }
    
}
