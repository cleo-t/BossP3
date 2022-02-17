using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGhostSuicidePact : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpookyStuff.ghostDead += this.Suicide;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Suicide()
    {
        Destroy(this.gameObject);
    }
}
