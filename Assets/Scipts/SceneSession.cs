using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSession : MonoBehaviour
{
    private void Awake()
    {
        int numSceneSessions = FindObjectsOfType<SceneSession>().Length;
        if (numSceneSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void ResetSceneSession()
    {
        Destroy(gameObject);
    }
}
