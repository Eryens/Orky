using GameToolkit.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour {

    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
        Save.instance.SaveVolume(volume);
    }

    public void SetLanguage(int languageIndex)
    {
        //Debug.Log(languageIndex);
        if (languageIndex == 0)
        {
            FindObjectOfType<AudioManager>().Play("MenuSound");
            Localization.Instance.CurrentLanguage = SystemLanguage.English;
        }
        else if (languageIndex == 1)
        {
            FindObjectOfType<AudioManager>().Play("MenuSound");
            Localization.Instance.CurrentLanguage = SystemLanguage.French;
        }
        Save.instance.SaveLanguage(languageIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        FindObjectOfType<AudioManager>().Play("MenuSound");
        Screen.fullScreen = isFullScreen;
        Save.instance.SaveFullScreen(isFullScreen);
    }
}
