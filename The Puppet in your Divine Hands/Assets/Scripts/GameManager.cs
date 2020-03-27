using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public LevelLoader levelLoader;
    public GameObject PauseGame;
    public GameObject GameOver;
    public bool isPaused = false;

    private GameObject player;
    private float addTimeMusic;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    private void Update()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            PauseGame.SetActive(true);
        }
        else
        {
            PauseGame.SetActive(false);
            Time.timeScale = 1;
        }

        if (player.GetComponent<Player>().isDead)
        {
            StartCoroutine(Dying());
        }
    }

    IEnumerator Dying()
    {
        player.GetComponent<Player>().OnFire = false;
        player.GetComponent<Animator>().SetBool("Dead", player.GetComponent<Player>().isDead);
        player.GetComponent<Rigidbody2D>().gravityScale = 3;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(3f);
        GameOver.SetActive(true);
    }

    public void ContinueGame()
    {
        isPaused = false;
        this.GetComponent<AudioSource>().UnPause();
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        isPaused = false;
        levelLoader.StartGame("MainMenu");
    }

    public void RestartLevel()
    {
        levelLoader.StartGame("Game");
    }

    public void ExitGame()
    {
        Debug.Log("Quiting game");
        Application.Quit();
    }

    public void GoToCredits()
    {
        levelLoader.StartGame("Credits");
    }

    public void PlayerCanMove()
    {
        player.GetComponent<Player>().CanMove = !player.GetComponent<Player>().CanMove;
    }
}
