using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour {

    [SerializeField]
    private int levelCompleted = 0;

    [HideInInspector]
    public static StoryManager instance;

    public float RestartDelay = 2f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public int GetIndexOfBattleGround()
    {
        if (levelCompleted > 7)
        {
            return 14;
        }
        else if (levelCompleted > 6)
        {
            return 3;
        }
        else if (levelCompleted > 4)
        {
            return 13;
        }
        else if (levelCompleted > 3)
        {
            return 12;
        }
        else
        {
            return 3;
        }
    }

    public void CompleteLevel()
    {
        levelCompleted += 1;
    }

    public int GetLevel()
    {
        return levelCompleted;
    }

    public void SetLevel(int level)
    {
        levelCompleted = level;
    }

    public int GetIndexOfTownInStory()
    {
        if (levelCompleted > 8)
        {
            return 11;
        }
        switch (levelCompleted)
        {
            case 0:
                return 2;
            case 1:
                return 4;
            case 2:
                return 5;
            case 3:
                return 6;
            case 4:
                return 7;
            case 5:
                return 8;
            case 6:
                return 9;
            case 7:
                return 10;
            case 8:
                return 11;
            default:
                return 2;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        var regex = "Battleground";
        var match = Regex.Match(scene.name, regex, RegexOptions.IgnoreCase);
        if (match.Success)
        {
            if (!GameManager.instance.wasGameOver)
                StartCoroutine(PlayBgTheme());
            Debug.Log("In a battleground : " + scene.name);
            ChangeBattleGroundDifficulty();
        }
    }

    private IEnumerator PlayBgTheme()
    {
        yield return new WaitForSeconds(1f);
        if (levelCompleted == 0)
        {
            FindObjectOfType<AudioManager>().PlayTheme("BgTheme2");
        }
        else if (levelCompleted > 0 )
        {
            FindObjectOfType<AudioManager>().PlayTheme("BgTheme1");
        }

    }

    private void ChangeBattleGroundDifficulty()
    {
        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        if (levelCompleted >= 0 && levelCompleted < 2)
        {
            levelManager.minWaveSpawnIndex = 1;
            levelManager.maxWaveSpawnIndex = 4;
        }
        else if (levelCompleted >= 2 && levelCompleted < 4)
        {
            levelManager.minWaveSpawnIndex = 3;
            levelManager.maxWaveSpawnIndex = 4;
        }
        else if (levelCompleted >= 4 && levelCompleted < 6)
        {
            levelManager.minWaveSpawnIndex = 3;
            levelManager.maxWaveSpawnIndex = 6;
        }
        else if (levelCompleted >= 6 && levelCompleted < 8)
        {
            levelManager.minWaveSpawnIndex = 5;
            levelManager.maxWaveSpawnIndex = 6;
        }
        else
        {
            levelManager.minWaveSpawnIndex = 6;
            levelManager.maxWaveSpawnIndex = 7;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
