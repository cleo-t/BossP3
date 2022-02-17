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
    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.FindGameObjectWithTag("Ghost").transform.position;
        center.z -= z_offset;
        center.y = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerSpotted)
        {
            MoveVision();
        }
        
    }

    void MoveVision()
    {
        float x = Mathf.Sin(rotationSpeed * Time.time) * x_oscillation_magnitude;

        float z = Mathf.Cos(rotationSpeed * Time.time) * z_oscillation_magnitude;

        transform.position = new Vector3(center.x + x, center.y, center.z + z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int layerMask = LayerMask.GetMask("Ghost");
            layerMask = ~layerMask;

            Ray r = new Ray(GameObject.FindGameObjectWithTag("Ghost").transform.position, GameObject.FindGameObjectWithTag("Player").transform.position - GameObject.FindGameObjectWithTag("Ghost").transform.position);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, 500, layerMask))
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
            } else
            {
                playerSpotted = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerSpotted = false;
        }
    }
}
