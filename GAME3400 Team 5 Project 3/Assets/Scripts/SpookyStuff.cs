using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookyStuff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("GhostSight").transform);
    }
}
