using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookyStuff : MonoBehaviour
{
    public int ghostHealth = 500;
    public float damageInterval = 10f;


    private float nextDamageTime = 0f;
    private bool canTakeDamage = true;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("GhostSight").transform);

    }

    public void AttackPlayer()
    {
        
    }

    public void TakeDamage() 
    {
        if (canTakeDamage && Time.time >= nextDamageTime)  
        {
            nextDamageTime = Time.time + 10f;
            ghostHealth -= 50;

            if (ghostHealth <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
