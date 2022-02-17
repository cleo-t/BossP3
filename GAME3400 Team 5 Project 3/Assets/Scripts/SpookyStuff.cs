using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookyStuff : MonoBehaviour
{
    public AudioClip GhostMoan;
    public AudioClip GhostSpot;
    public AudioClip GhostShot;

    public int ghostHealth = 500;
    public float damageInterval = 10f;

    public static event Action ghostDead;

    [SerializeField]
    private Color vulnerableColor;

    private Color initialColor;

    private float nextDamageTime = 0f;
    private bool canTakeDamage
    {
        get
        {
            return Time.time > this.nextDamageTime;
        }
        set
        {

        }
    }

    private Renderer renderComp;

    // Start is called before the first frame update
    void Start()
    {
        this.renderComp = this.GetComponent<Renderer>();
        this.initialColor = this.renderComp.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("GhostSight").transform);
        this.renderComp.material.color = this.canTakeDamage ? this.vulnerableColor : this.initialColor;
    }

    public void AttackPlayer()
    {
        
    }

    public void TakeDamage() 
    {
        if (this.canTakeDamage)  
        {
            AudioSource.PlayClipAtPoint(GhostShot, this.transform.position);
            nextDamageTime = Time.time + damageInterval; 
            ghostHealth -= 50;

            if (ghostHealth <= 0) {
                ghostDead.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
