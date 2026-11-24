using UnityEngine;
using UnityEngine.AI;

public class PickItemUp : MonoBehaviour
{
    [SerializeField] public bool isHammer; // Flag to indicate if this pickup is a hammer
    [SerializeField] private float timeToDestroy = 5f;
    [SerializeField] private float timer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        isHammer = false; // Initialize the flag to false
        timer   = 0f;
         // Set the agent's radius to a smaller value
    }

    // Update is called once per frame
    void Update()
    {
        if (isHammer)
        {
            timer += Time.deltaTime;
            if (timer >= timeToDestroy)
            {
                isHammer = false;
                timer = 0f;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hammer"))
        {
            isHammer = true;
        }
    }
    public void hammerSmash()
    {
        
        if (isHammer)
        {
            //play smash animation
            //play sound effect
            //stun();
            isHammer = false;
        }

    }

}
