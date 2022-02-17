using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSightMovement : MonoBehaviour
{
    Vector3 center;
    public float x_oscillation_magnitude = 30;
    public float z_oscillation_magnitude = 15;
    public float rotationSpeed = 1;
    public float z_offset = 5;

    static public bool playerSpotted = false;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.FindGameObjectWithTag("Ghost").transform.position;
        center.z -= z_offset;
        center.y = 0.0f;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerSpotted)
        {
            timer += Time.deltaTime;
            MoveVision();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, 0.5f);
        }
        
    }

    void MoveVision()
    {
        float x = Mathf.Sin(rotationSpeed * timer) * x_oscillation_magnitude;

        float z = Mathf.Cos(rotationSpeed * timer) * z_oscillation_magnitude;

        transform.position = new Vector3(center.x + x, center.y, center.z + z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int layerMask = LayerMask.GetMask("Ghost");
            layerMask = ~layerMask;

            Vector3 headCheck = GameObject.FindGameObjectWithTag("Player").transform.position;
            headCheck.y = GameObject.FindObjectOfType<CharacterController>().height * GameObject.FindGameObjectWithTag("Player").transform.localScale.y;

            Ray r1 = new Ray(GameObject.FindGameObjectWithTag("Ghost").transform.position, GameObject.FindGameObjectWithTag("Player").transform.position - GameObject.FindGameObjectWithTag("Ghost").transform.position);
            Ray r2 = new Ray(GameObject.FindGameObjectWithTag("Ghost").transform.position, headCheck - GameObject.FindGameObjectWithTag("Ghost").transform.position);
            RaycastHit hit;
            RaycastHit hit2;
            if (Physics.Raycast(r1, out hit, 500, layerMask))
            {
                if (hit.collider.tag == "Player")
                {
                    // Play sound
                    
                }
            }

            if (Physics.Raycast(r2, out hit2, 500, layerMask))
            {
                if (hit2.collider.tag == "Player")
                {
                    // Play sound

                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int layerMask = LayerMask.GetMask("Ghost");
            layerMask = ~layerMask;

            Vector3 headCheck = GameObject.FindGameObjectWithTag("Player").transform.position;
            headCheck.y = GameObject.FindObjectOfType<CharacterController>().height * GameObject.FindGameObjectWithTag("Player").transform.localScale.y;

            Ray r1 = new Ray(GameObject.FindGameObjectWithTag("Ghost").transform.position, GameObject.FindGameObjectWithTag("Player").transform.position - GameObject.FindGameObjectWithTag("Ghost").transform.position);
            Ray r2 = new Ray(GameObject.FindGameObjectWithTag("Ghost").transform.position, headCheck - GameObject.FindGameObjectWithTag("Ghost").transform.position);
            RaycastHit hit;
            RaycastHit hit2;
            if (Physics.Raycast(r1, out hit, 500, layerMask))
            {
                if (hit.collider.tag == "Player")
                {
                    // Attack the player
                    playerSpotted = true;
                    FindObjectOfType<SpookyStuff>().AttackPlayer();
                }
                else
                {
                    playerSpotted = false;
                }
            }
            else
            {
                playerSpotted = false;
            }

            if (Physics.Raycast(r2, out hit2, 500, layerMask))
            {
                if (hit2.collider.tag == "Player")
                {
                    // Attack the player
                    playerSpotted = true;
                    FindObjectOfType<SpookyStuff>().AttackPlayer();
                }
                else
                {
                    playerSpotted = false;
                }
            }
            else
            {
                playerSpotted = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerSpotted = false;
        }
    }
}
