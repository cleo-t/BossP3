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
    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.FindGameObjectWithTag("Ghost").transform.position;
        center.z -= z_offset;
        center.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Sin(rotationSpeed * Time.time) * x_oscillation_magnitude;

        float z = Mathf.Cos(rotationSpeed * Time.time) * z_oscillation_magnitude;

        transform.position = new Vector3(center.x + x, center.y, center.z + z);    
    }
}
