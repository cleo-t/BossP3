using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void OnTriggerEnter(Collider other)
    {
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
