using System.Collections.Generic;
using UnityEngine;

public class SpotlightManager : MonoBehaviour
{
    [SerializeField] private GameObject Spotlights;
    [SerializeField] private GameObject floor;
    public List<TempBenPlayerboolcheck> players;

    private void Start()
    {

        for (int i = 1; i < 3; i++)
        {
            Instantiate(
                Spotlights,
                floor.transform.position + new Vector3(0, 8, 0),
                Quaternion.Euler(90, 0, 0)
            );
        }
    }
}