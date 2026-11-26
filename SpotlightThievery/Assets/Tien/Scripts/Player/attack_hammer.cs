using UnityEngine;
using UnityEngine.InputSystem;

public class attack_hammer : MonoBehaviour
{
    //[SerializeField] private GameObject hammerRange;
    [SerializeField] private GameObject hammer;
    private Collider hammerRangeCollider;
    

    [SerializeField] private GameObject player;
    [SerializeField] private float timer = 0.1f;
    

    [SerializeField] private float attackDelay = 0.1f; //delay time for the attack, can be adjusted in the inspector
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //hammerRangeCollider = hammerRange.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //hammerRangeCollider.enabled = false;
            hammer.SetActive(false);
        }
    }
    public void attack()
    {
        hammer.SetActive(true);
        //hammerRangeCollider.enabled = true;
        timer = attackDelay;
    }
}
