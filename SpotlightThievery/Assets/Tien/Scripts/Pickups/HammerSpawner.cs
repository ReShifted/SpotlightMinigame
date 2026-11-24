using UnityEngine;
using UnityEngine.Rendering;

public class HammerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Ground;
    [SerializeField] private GameObject hammerPrefab;
    private Bounds groundBound;
    [SerializeField] private bool spawnHammers = true;

    

    [SerializeField] private float spawnInterval;
    [SerializeField] private int maxSpawnInterval = 10;
    [SerializeField] private int minSpawnInterval = 7;
    [SerializeField] private float randomNum;

    [SerializeField] private int minHammers = 1; // Minimum number of hammers allowed in the scene
    [SerializeField] private int maxHammers = 4; // Maximum number of hammers allowed in the scene

    [SerializeField] private float spawnDelay = 2f; // Delay before the first spawn
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundBound = Ground.GetComponent<Collider>().bounds;
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval); // Initialize the spawn interval
    }

    // Update is called once per frame
    void Update()
    {
        randomNum = Random.Range(minSpawnInterval, maxSpawnInterval);
        if (spawnDelay > 0)
            spawnDelay -= Time.deltaTime;

        if (spawnHammers)
        {
            if (spawnDelay <= 0)
            {
                SpawnHammers();
                spawnHammers = false;
            }
        }
        if (spawnInterval <= 0)
        {
            spawnHammers = true;
            spawnInterval = randomNum; // Reset the spawn interval
        }
        if (spawnHammers == false)
            spawnInterval -= Time.deltaTime;


    }
    private static Vector3 RandomPointToSpawn(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.max.y,
            Random.Range(bounds.min.z, bounds.max.z));
    }
    private void SpawnHammers()
    {
        for (int i = 0; i < Random.Range(minHammers,maxHammers); i++)
        {       
            Vector3 spawnPosition = RandomPointToSpawn(groundBound);
            Vector3 targetSpawnPosition =  new Vector3(spawnPosition.x, spawnPosition.y + 0.5f, spawnPosition.z); // Adjust the y position to be above the ground
            Instantiate(hammerPrefab, targetSpawnPosition, Quaternion.identity);
            
        }
    }

}
