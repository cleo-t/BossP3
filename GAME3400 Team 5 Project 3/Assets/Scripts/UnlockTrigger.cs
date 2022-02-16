using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject thingToDelete;
    [SerializeField]
    private AudioClip unlockSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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

        Destroy(this.gameObject);
    }
}