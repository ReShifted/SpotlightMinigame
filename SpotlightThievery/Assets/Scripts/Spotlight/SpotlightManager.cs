using System.Collections.Generic;
using UnityEngine;

public class SpotlightManager : MonoBehaviour   
{
    [SerializeField] private GameObject Spotlights;
    [SerializeField] private GameObject floor;
    public List<GameObject> players;

    private void Awake()
    {

        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

        for (int i = 1; i < players.Count; i++)
        {
            Instantiate(
                Spotlights,
                floor.transform.position + new Vector3(0, 8, 0),
                Quaternion.Euler(90, 0, 0)
            );
        }
    }
}