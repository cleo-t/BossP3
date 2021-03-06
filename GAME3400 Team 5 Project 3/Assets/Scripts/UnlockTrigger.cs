using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockTrigger : MonoBehaviour
{
    public static event Action TriggerUnlocked;

    [SerializeField]
    private GameObject thingToDelete;
    [SerializeField]
    private AudioClip unlockSound;

    private FPSPlayer player;

    private void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && this.player.crouching)
        {
            this.Unlock();
        }
    }

    private void Unlock()
    {
        Destroy(this.thingToDelete);
        if (this.unlockSound != null)
        {
            AudioSource.PlayClipAtPoint(this.unlockSound, this.transform.position);
        }

        if (TriggerUnlocked != null)
        {
            Debug.Log("Event");
            TriggerUnlocked.Invoke();
        }

        Destroy(this.gameObject);
    }
}
