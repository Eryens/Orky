using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void Start()
    {
        FindObjectOfType<AudioManager>().PlayTheme("MainMenu");
    }

    public void PlayGame()
    {
        Save.instance.LoadGame();
        FindObjectOfType<AudioManager>().Play("MenuSound");
    }

    public void NewGame()
    {
        LevelChangerScript.instance.FadeToLevel(2);
        FindObjectOfType<AudioManager>().Play("MenuSound");
    }

    public void QuitGame()
    {
        //Debug.Log("quit");
        Application.Quit();
    }

    public void PlayMenuSound()
    {
        FindObjectOfType<AudioManager>().Play("MenuSound");
    }
}
