using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public static GameManager instance;

    [HideInInspector]
    public bool isInDialogue = false;
    [HideInInspector]
    public bool isInPause = false;

    public float RestartDelay = 2f;

    [HideInInspector]
    public bool wasGameOver = false;

    GameObject pauseMenu;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (!isInDialogue && (Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Escape)))
        {
            if (!isInPause)
                TriggerPauseMenu();
            else
                EndPauseMenu();
        }
	}

    public void EndPauseMenu()
    {
        FindObjectOfType<AudioManager>().Play("MenuSound");
        Time.timeScale = 1f;
        isInPause = false;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        FindObjectOfType<AudioManager>().UnPauseTheme();
    }

    public void TriggerPauseMenu()
    {
        //Debug.Log("Pause menu triggered");
        FindObjectOfType<AudioManager>().Play("MenuSound");
        Time.timeScale = 0f;
        isInPause = true;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        FindObjectOfType<AudioManager>().PauseTheme();
    }

    private void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    void Resume()
    {

    }

    public void GameOver()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        LevelChangerScript.instance.FadeToLevel(SceneManager.GetActiveScene().buildIndex, false);
        wasGameOver = true;
    }

    public void SpecialGameOver()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        LevelChangerScript.instance.FadeToLevel(SceneManager.GetActiveScene().buildIndex, false);
        wasGameOver = true;
    }

    public void Victory()
    {
        FindObjectOfType<AudioManager>().FadeOutTheme();
        GameObject victoryzone = GameObject.FindGameObjectWithTag("VictoryZone");
        StoryManager.instance.CompleteLevel();
        DialogOnInteraction dialogueScript = victoryzone.GetComponent<DialogOnInteraction>();
        DialogueManager.instance.StartDialogue(dialogueScript.dialogue);
        LevelChangerScript.instance.FadeToLevel(StoryManager.instance.GetIndexOfTownInStory());
        FindObjectOfType<AudioManager>().Play("Victory");
        Save.instance.SaveGame();
        wasGameOver = false;
    }

    public void GoToBattle()
    {
        LevelChangerScript.instance.FadeToBattleField();
    }

    public void TriggerDialoguePause()
    {
        isInDialogue = true;
        Time.timeScale = 0f;
    }

    public void EndDialoguePause()
    {
        Time.timeScale = 1f;
        isInDialogue = false;
    }
}
