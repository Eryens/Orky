using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup audioMixer;

    public Sound[] sounds;

    private Sound currentTheme ;

    public static AudioManager instance;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = audioMixer;
        }
    }

    private void Start()
    {
    }
    
    public void FadeOutTheme()
    {
        if (currentTheme != null)
        {
            float startVolume = currentTheme.source.volume;
            StartCoroutine(FadeOut(startVolume));
        }
    }

    IEnumerator FadeOut(float startVolume)
    {
        while (currentTheme.source.volume > 0)
        {
            currentTheme.source.volume -= startVolume * Time.deltaTime ;
            yield return null;
        }

        currentTheme.source.Stop();
        currentTheme.source.volume = startVolume;
    }

    public void PlayTheme(string themeName)
    {
        Sound s = Array.Find(sounds, sound => sound.SoundName == themeName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + themeName + " not found");
        }
        StartCoroutine(DelayTheme(s, themeName));
    }

    private IEnumerator DelayTheme(Sound theme, string themeName)
    {
        //Debug.Log("delay theme");
        yield return null ;
        currentTheme = theme;
        Play(themeName);
    }

    public void PlayTownTheme()
    {
        if (StoryManager.instance.GetLevel() == 0)
        {
            PlayTheme("TownTheme1");
        }
        else
        {
            PlayTheme("TownTheme2");
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.SoundName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
        }
        if (s != null)
        {
            s.source.Play();
        }

    }

    public void PauseTheme()
    {
        currentTheme.source.Pause();
    }

    public void UnPauseTheme()
    {
        currentTheme.source.UnPause();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.SoundName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
        }
        s.source.Pause();
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.SoundName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
        }
        s.source.UnPause();
    }
}
