using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public int minWaveSpawnIndex = 6;
    public int maxWaveSpawnIndex = 8;
    private int spawnIndex;

    [SerializeField]
    private GameObject dork;

    public float topYCoordinates = 1.50f;
    public float bottomYCoordinates = -3.50f;

    public float timeBetweenWaves = 5f;

    public int[] spawnChances;

    [SerializeField]
    private GameObject[] enemies;
    private int selectedEnemy;

    [SerializeField]
    private float[] enemiesSpawnWeight;

    private List<GameObject> thisWaveOfEnemies = new List<GameObject>();

	// Use this for initialization
	void Start () {
        if (enemies.Length != enemiesSpawnWeight.Length)
        {
            Debug.LogError("Enemies and spawn index are not of equal size, which will results in heavy bugs");
        }
        if (enemies.Length != spawnChances.Length)
        {
            Debug.LogError("Enemies and spawn chances are not of equal size, which will results in heavy bugs");
        }
        InvokeRepeating("CreateWave", 1f, timeBetweenWaves);
    }

    // Update is called once per frame
    void Update () {

	}

    private void CreateWave()
    {
        thisWaveOfEnemies = new List<GameObject>();
        spawnIndex = GenerateWaveSpawnIndex();

        float i = 0;
        while (i < spawnIndex)
        {
            SpawnEnemy();

            if (enemiesSpawnWeight[selectedEnemy] != 0 )
                i += enemiesSpawnWeight[selectedEnemy];
            else
            {
                i += 1f;
                Debug.LogError("An enemy has a spawn weight of 0, it has been automatically set to 1. " +
                               "OR No enemy has been selected");
            }
        }
        SetOrderInLayerOfEnemies();
    }

    private void SetOrderInLayerOfEnemies()
    {
        List<GameObject> sorted = thisWaveOfEnemies.OrderBy(o => o.transform.position.y).ToList();
        int orderInLayer = 0;
        foreach (GameObject go in sorted)
        {
            SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();
            if (sprite != null)
            {
                sprite.sortingOrder = orderInLayer;
                orderInLayer--;
            }
        }
    }

    void SpawnEnemy()
    {
        selectedEnemy =SelectEnemy();
        //Debug.Log("Selected enemy: " + selectedEnemy);
        Vector3 spawnCoordinates = GenerateCoordinates();
        var instanciated = Instantiate(enemies[selectedEnemy], spawnCoordinates, Quaternion.identity);
        thisWaveOfEnemies.Add(instanciated);
    }

    private Vector3 GenerateCoordinates()
    {
        float xOffSet = UnityEngine.Random.Range(1.0f, 6.0f);
        float yCoordinates = UnityEngine.Random.Range(bottomYCoordinates, topYCoordinates + 1f);
        float xCoordinates = dork.transform.position.x + (Camera.main.orthographicSize * Screen.width / Screen.height) + 10 + xOffSet;

        return new Vector3(xCoordinates, yCoordinates, 0);
    }

    private int SelectEnemy()
    {
        int totalSpawnChance = 0;
        for (int i = 0; i < spawnChances.Length; ++i)
        {
            totalSpawnChance += spawnChances[i];
        }
        // Nb aléatoire entre 1 et 100
        int selectedPercentage = UnityEngine.Random.Range(1, totalSpawnChance + 1);
        int calculWeight = 0;
        for (int i = 0; i < spawnChances.Length; ++i)
        {
            calculWeight += spawnChances[i];
            if (calculWeight - selectedPercentage >= 0)
            {
                return i;
            }
        }
        Debug.LogError("Something went wrong in SelectEnemy()");
        return 0;
    }

    // Les spawns index doivent êtres modifiés selon la difficulté du level de manière logarithmique
    private int GenerateWaveSpawnIndex()
    {
        return UnityEngine.Random.Range(minWaveSpawnIndex, maxWaveSpawnIndex + 1);
    }
}
