using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    [SerializeField]
    private string nextScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.LoadScene();
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(this.nextScene);
    }
}
