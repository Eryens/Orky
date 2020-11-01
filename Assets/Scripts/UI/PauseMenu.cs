using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public void ResumeGame()
    {
        FindObjectOfType<AudioManager>().Play("MenuSound");
        GameManager.instance.EndPauseMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
