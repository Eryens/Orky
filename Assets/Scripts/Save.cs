using GameToolkit.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Save : MonoBehaviour {

    [HideInInspector]
    public static Save instance;

    public AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("level", StoryManager.instance.GetLevel());
    }

    public void LoadGame()
    {
        int level = PlayerPrefs.GetInt("level");
        if (level != 0)
        {
            StoryManager.instance.SetLevel(level);
            LevelChangerScript.instance.FadeToLevel(StoryManager.instance.GetIndexOfTownInStory());
        }
        else
        {
            LevelChangerScript.instance.FadeToLevel(2);
        }
    }

    public void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void SaveFullScreen(bool fullScreen)
    {
        PlayerPrefs.SetString("fullScreen", fullScreen.ToString());
    }

    public void SaveLanguage(int language)
    {
        PlayerPrefs.SetInt("language", language);
    }

    public void LoadOptions()
    {
        string fullString = PlayerPrefs.GetString("fullScreen");
        if (fullString != "")
        {
            bool isFullScreen = bool.Parse(fullString);
            Debug.Log("Full screen " + isFullScreen);
            Screen.fullScreen = isFullScreen;
        }
        if (PlayerPrefs.GetFloat("volume") != -80)
        {
            audioMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("volume"));
        }
        Debug.Log("langue " + PlayerPrefs.GetInt("language"));
        if (PlayerPrefs.GetInt("language") == 0)
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.English;
        }
        else if (PlayerPrefs.GetInt("language") == 1)
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.French;
        }

    }
}
