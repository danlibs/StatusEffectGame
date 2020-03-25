using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;
    public bool FirstPlay = true;

    private PlayableDirector[] playableDirectors;
    private GameObject sounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            sounds = FindObjectOfType<GameManager>().gameObject;
            playableDirectors = GameObject.FindGameObjectWithTag("LevelManager").GetComponents<PlayableDirector>();

            if (FirstPlay)
            {
                PlayCutscene(0);
                sounds.GetComponent<AudioSource>().enabled = false;
                sounds.GetComponent<Animator>().enabled = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CanMove = false;
            }
        } else return;

    }

    public void NotFirstPlay()
    {
        FirstPlay = false;
        sounds.GetComponent<AudioSource>().enabled = true;
        sounds.GetComponent<Animator>().enabled = true;
    }

    public void PlayCutscene(int cutsceneIndex)
    {
        playableDirectors[cutsceneIndex].Play();

    }
}
