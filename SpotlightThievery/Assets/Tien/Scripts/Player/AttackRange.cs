using UnityEngine;
using System.Collections.Generic;
public class AttackRange : MonoBehaviour
{
    private List<Collider> playersInRange = new List<Collider>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Enemy entered attack range" + playersInRange.Count);
             playersInRange.Add(other);
            // Add logic to handle enemy entering attack range
        }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Enemy exited attack range" + playersInRange.Count);
                // Add logic to handle enemy exiting attack range
                playersInRange.Remove(other);
            }
        }
}
