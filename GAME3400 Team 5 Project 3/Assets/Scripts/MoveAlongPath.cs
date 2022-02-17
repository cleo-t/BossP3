using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{
    [SerializeField]
    private List<Transform> controlPoints;
    [SerializeField]
    private float moveSeconds = 10;

    private bool started;

    void Start()
    {
        this.started = false;
        UnlockTrigger.TriggerUnlocked += this.StartPath;
    }

    void StartPath()
    {
        if (!this.started)
        {
            Debug.Log("Started");
            StartCoroutine(Move());
        }
        this.started = true;
    }

    private IEnumerator Move()
    {
        float timer = 0;
        while (timer < this.moveSeconds)
        {
            timer += Time.deltaTime;
            if (timer >= this.moveSeconds)
            {
                continue;
            }
            float t = timer / this.moveSeconds;
            int index = (int)(t * (float)(this.controlPoints.Count));
            int nextIndex = index + 1;

            float tPerPath = 1.0f / (float)this.controlPoints.Count;
            float timeOffset = tPerPath * index;
            float progressInPath = (t - timeOffset) / tPerPath;

            this.transform.position = Vector3.Lerp(this.controlPoints[index].position, this.controlPoints[nextIndex].position, progressInPath);

            yield return null;
        }
        this.transform.position = this.controlPoints[this.controlPoints.Count - 1].position;
    }
}
