using UnityEngine;
using System.Collections;
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
    public GameObject enemyPrefab;
    [Space]
    [HideInInspector] public int enemiesCount = 0;
    private int enemiesToSpawn = 5;
    //private int enemiesSpawned;
    private int maxEnemiesToSpawn = 50;

    void Start()
    {
        StartCoroutine(Game());
    }

    void LateUpdate()
    {
        //If player killed all enemies => break
        if (isEnemiesSpawned && enemiesCount == 0)
        {
            StopCoroutine(Game());
            StartCoroutine(Game());
            isEnemiesSpawned = false;
        }
    }

    public IEnumerator Game()
    {
        while (true)
        {
            //Break
            isWave = false;
            UIManager.Instance.StartCoroutine(UIManager.Instance.BreakTimer());

            nowWave++;
            UIManager.Instance.ChangeWaveCount(nowWave);
            Debug.Log("Now wave is " + nowWave);
            yield return new WaitForSeconds(breakTime);
            //Wave
            isWave = true;
            UIManager.Instance.StartCoroutine(UIManager.Instance.WaveTimer());

            if (enemiesToSpawn + 5 < maxEnemiesToSpawn)
                enemiesToSpawn += 5;
            StartCoroutine(EnemiesSpawn(enemiesToSpawn));

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
        Vector2 spawnPos = new Vector2(WorldBounds.Instance.b_Max.position.x, Random.Range(WorldBounds.Instance.b_Max.position.y, WorldBounds.Instance.b_Min.position.y));
        GameObject.Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation, enemiesParent);
    }
}