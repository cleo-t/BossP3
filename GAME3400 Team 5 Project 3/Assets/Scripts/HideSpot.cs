using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HideSpot : MonoBehaviour
{
    public static bool playerInAnyHideSpot
    {
        get
        {
            return playerInSpotCount > 0;
        }
        private set
        {

        }
    }

    private static int playerInSpotCount;

    private bool playerInThisSpot;

    private void Awake()
    {
        playerInSpotCount = 0;
    }

    void Start()
    {
        this.playerInThisSpot = false;
        MeshCollider parentMeshCollider = this.GetComponentInParent<MeshCollider>();
        Mesh parentMesh = parentMeshCollider.sharedMesh;
        MeshCollider myMeshCollider = this.gameObject.AddComponent<MeshCollider>();
        myMeshCollider.convex = true;
        myMeshCollider.isTrigger = true;
        myMeshCollider.sharedMesh = parentMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("Player"))
        {
            this.playerInThisSpot = true;
            playerInSpotCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.playerInThisSpot)
            {
                playerInSpotCount--;
            }
            this.playerInThisSpot = false;
        }
    }
}
