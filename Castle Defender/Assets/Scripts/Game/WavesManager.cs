using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WavesManager : MonoBehaviour
{
    public bool isWave = false;
    bool isEnemiesSpawned = false;
    [SerializeField] private int nowWave = 0;
    [Space]
    public float waveTime;
    public float breakTime;
    [Space]
    public float spawnRate;
    [Space]
    public Transform enemiesParent;
    public GameObject[] enemiesPrefabs;
    private List<GameObject> availableEnemies = new List<GameObject>();

    [Space]
    [HideInInspector] public int enemiesCount = 0;
    private int enemiesToSpawn = 5;

    private int maxEnemiesToSpawn = 25;

    void Start()
    {
        StartCoroutine(Game());
    }

    void LateUpdate()
    {
        //If player killed all enemies => break
        if (isEnemiesSpawned && enemiesCount == 0)
        {
            StopAllCoroutines();
            StartCoroutine(Game());
        }
    }

    public IEnumerator Game()
    {
        while (true)
        {
            //Break
            isEnemiesSpawned = false;
            //Setting Lights
            LightsManager.Instance.SetDayLight();

            isWave = false;
            
            UIManager.Instance.StartCoroutine(UIManager.Instance.BreakTimer());

            nowWave++;
            UIManager.Instance.ChangeWaveCount(nowWave);

            //Adding new types of enemies to the wave
            CheckForAvailableEnemies();

            Music.Instance.StartFade();
            yield return new WaitForSeconds(breakTime);
            //Wave

            //Setting Lights
            LightsManager.Instance.SetNightLight();

            isWave = true;
            
            UIManager.Instance.StartCoroutine(UIManager.Instance.WaveTimer());

            if (enemiesToSpawn + 5 < maxEnemiesToSpawn)
                enemiesToSpawn += 3;
            StartCoroutine(EnemiesSpawn(enemiesToSpawn));

            Music.Instance.StartFade();
            yield return new WaitForSeconds(waveTime);
        }
    }

    IEnumerator EnemiesSpawn(int countToSpawn)
    {
        int enemiesSpawned = 0;
        isEnemiesSpawned = false;
        while (enemiesSpawned < countToSpawn)
        {
            SpawnEnemy();
            enemiesCount++;
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnRate);
        }

        isEnemiesSpawned = true;
    }

    void SpawnEnemy()
    {
        GameObject enemyToSpawn = availableEnemies[Random.Range(0, availableEnemies.Count)];

        Vector2 spawnPos = new Vector2(WorldBounds.Instance.b_Max.position.x, Random.Range(WorldBounds.Instance.b_Max.position.y, WorldBounds.Instance.b_Min.position.y));
        GameObject.Instantiate(enemyToSpawn, spawnPos, enemyToSpawn.transform.rotation, enemiesParent);
    }

    void CheckForAvailableEnemies()
    {
        foreach (GameObject enemyGO in enemiesPrefabs)
            if (nowWave == enemyGO.GetComponent<Enemy>().minWave)
                availableEnemies.Add(enemyGO);
    }
}