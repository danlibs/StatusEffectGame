using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class MainMenu : MonoBehaviour
{
    public LevelLoader LevelLoader;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OpeningWalk()
    {
        player.GetComponent<PlayableDirector>().Play();
    }

    public void StartGame()
    {
        LevelLoader.StartGame("Game");
    }

    public void QuitGame()
    {
        Debug.Log("Exiting game");
        Application.Quit();
    }

}
