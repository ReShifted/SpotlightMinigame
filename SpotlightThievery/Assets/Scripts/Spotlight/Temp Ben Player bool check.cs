using UnityEngine;

public class TempBenPlayerboolcheck : MonoBehaviour
{
    public bool safeForNextRound;
    public bool insideSpotlight;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        safeForNextRound = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInsideSpotlight(bool value)
    {
        insideSpotlight = value;
        safeForNextRound = value;
    }
    
}
