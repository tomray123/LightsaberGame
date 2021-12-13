using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataInitializer : MonoBehaviour
{
    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }

    protected void OnAwake()
    {
        List<IAwakeInit> data = new List<IAwakeInit>();
        Scene currScene = SceneManager.GetActiveScene();
        GameObject[] rootObjects = currScene.GetRootGameObjects();

        foreach (var go in rootObjects)
        {
            data.AddRange(go.GetComponentsInChildren<IAwakeInit>(true));
        }
            
        foreach (var script in data)
        {
            script.OnAwakeInit();
        }
    }

    protected void OnStart()
    {
        List<IStartInit> data = new List<IStartInit>();
        Scene currScene = SceneManager.GetActiveScene();
        GameObject[] rootObjects = currScene.GetRootGameObjects();

        foreach (var go in rootObjects)
        {
            data.AddRange(go.GetComponentsInChildren<IStartInit>(true));
        }

        foreach (var script in data)
        {
            script.OnStartInit();
        }
    }
}
