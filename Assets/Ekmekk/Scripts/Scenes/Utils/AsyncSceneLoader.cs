using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{
    public float Progress { get; private set; }

    public void LoadScene(Scenes scene)
    {
        Progress = 0;
        StartCoroutine(AsyncLoadScene(scene));
    }

    private IEnumerator AsyncLoadScene(Scenes scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync((int) scene);

        while (!operation.isDone)
        {
            Progress = Mathf.Clamp01(operation.progress / .9f);

            yield return null;
        }
    }
}