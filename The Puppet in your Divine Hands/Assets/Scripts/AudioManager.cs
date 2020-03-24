using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    public static AudioManager Instance;

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

        foreach (Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
            s.source.loop = s.Loop;
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.SoundName == name);
        if (s == null)
        {
            Debug.LogWarning("There's no sound with the name: " + name + ".");
            return;
        }
        s.source.Play();
    } 

    public void StopPlay(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.SoundName == name);
        if (s == null)
        {
            Debug.LogWarning("There's no sound with the name: " + name + ".");
            return;
        }
        s.source.Stop();
    }
}
