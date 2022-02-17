using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWhenPotteryBreaks : MonoBehaviour
{
    [SerializeField]
    private Vector3 locationB;
    [SerializeField]
    private float moveSeconds = 5;

    private Vector3 locationA;
    private bool potBroken;

    void Start()
    {
        this.locationA = this.transform.position;
        this.potBroken = false;
        Pottery.PotDestroyed += this.OnPotBroken;
    }

    private void OnPotBroken()
    {
        if (!this.potBroken)
        {
            StartCoroutine(MoveToLocationB());
        }
        this.potBroken = true;
    }

    private float SCurveLerp(float t)
    {
        return (-Mathf.Cos(Mathf.PI * Mathf.Clamp(t, 0, 1)) + 1) / 2;
    }

    private IEnumerator MoveToLocationB()
    {
        float timer = 0;
        while (timer <= this.moveSeconds)
        {
            this.transform.position = Vector3.Lerp(this.locationA, this.locationB, this.SCurveLerp(timer / this.moveSeconds));
            timer += Time.deltaTime;
            yield return null;
        }
        this.transform.position = this.locationB;
    }
}
