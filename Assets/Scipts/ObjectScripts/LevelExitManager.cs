using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitManager : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(NextLevelRoutine());
        }
    }


    private IEnumerator NextLevelRoutine()
    {
        yield return new WaitForSeconds(3f);
        int currentStateIndex = SceneManager.GetActiveScene().buildIndex;
        FindObjectOfType<SceneSession>().ResetSceneSession();
        SceneManager.LoadScene(currentStateIndex + 1);

    }
}
