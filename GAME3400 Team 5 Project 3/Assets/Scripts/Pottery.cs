using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pottery : MonoBehaviour
{
    public static event Action PotDestroyed;

    public void TakeDamage()
    {
        if (PotDestroyed != null)
        {
            PotDestroyed.Invoke();
        }
        Destroy(this.gameObject);
    }
}
