using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;
    public TimelineAsset[] Timelines;
    public bool FirstPlay = true;
    public bool SomethingWasPressed = false;
    public bool IntroFinished = false;

    private PlayableDirector playableDirectors;
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
            playableDirectors = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PlayableDirector>();
            if (FirstPlay)
            {
                PlayCutscene(0);
                sounds.GetComponent<AudioSource>().enabled = false;
                sounds.GetComponent<Animator>().enabled = false;
                FirstPlay = false;
            }

            StartCoroutine(TimeIntroCutscene());

        } else return;

    }

    IEnumerator TimeIntroCutscene()
    {
        yield return new WaitUntil(() => IntroFinished == true);
        if (Input.anyKeyDown)
        {
            SomethingWasPressed = true;
        }
        StartSecretFinal();
    }

    public void StartSecretFinal()
    {
        StartCoroutine(SecretFinal());
    }

    IEnumerator SecretFinal()
    {
        yield return new WaitForSeconds(120f);
        if (SomethingWasPressed == false)
        {
            sounds.GetComponent<AudioSource>().enabled = false;
            sounds.GetComponent<Animator>().enabled = false;
            PlayCutscene(1);
            SomethingWasPressed = true;
        }
    }

    public void IntroCutsceneFinished()
    {
        IntroFinished = true;
    }

    public void NotFirstPlay()
    {
        sounds.GetComponent<AudioSource>().enabled = true;
        sounds.GetComponent<Animator>().enabled = true;
    }

    public void PlayCutscene(int index)
    {
        playableDirectors.Play(Timelines[index]);
        if (playableDirectors.playableAsset == Timelines[index])
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CanMove = false;
        }
        else return;
        
        
    }

    public void StopCutscene(int index)
    {
        playableDirectors.Stop();
    }
}
