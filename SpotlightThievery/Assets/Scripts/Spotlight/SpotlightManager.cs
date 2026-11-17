using UnityEngine;

public class SpotlightManager : MonoBehaviour
{
    [SerializeField] private GameObject Spotlights;
    [SerializeField] private GameObject floor;

    private void Start()
    {
        Instantiate(
            Spotlights,
            floor.transform.position + new Vector3(0, 10, 0),
            Quaternion.Euler(90, 0, 0)
        );
    }
}