using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator TransitionAnimator;

    public void StartGame(string sceneName)
    {
        StartCoroutine(GameStarting(sceneName));
    }

    IEnumerator GameStarting(string sceneName)
    {
        TransitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
