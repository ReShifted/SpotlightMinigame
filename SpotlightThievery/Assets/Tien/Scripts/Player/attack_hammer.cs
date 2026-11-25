using UnityEngine;
using UnityEngine.InputSystem;

public class attack_hammer : MonoBehaviour
{
    [SerializeField] private GameObject hammerRange;
    private Collider hammerRangeCollider;

    [SerializeField] private GameObject player;
    [SerializeField] private float timer = 0.1f;
    

    [SerializeField] private float attackDelay = 0.1f; //delay time for the attack, can be adjusted in the inspector
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hammerRangeCollider = hammerRange.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            hammerRangeCollider.enabled = false;
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame) //input system for attack
        {
            if (player.GetComponent<PickItemUp>().isHammer) //check if player has hammer
            {
                attack();
            }
        }
    }
    private void attack()
    {
        hammerRangeCollider.enabled = true;
        timer = attackDelay;
    }
}
