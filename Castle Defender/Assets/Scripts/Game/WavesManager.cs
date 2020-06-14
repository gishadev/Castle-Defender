using UnityEngine;
using System.Collections;
public class WavesManager : MonoBehaviour
{
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
    private int enemiesToSpawn = 5;
    //private int enemiesSpawned;
    private int maxEnemiesToSpawn = 50;

    void Start()
    {
        StartCoroutine(Game());
    }

    public IEnumerator Game()
    {
        while (true)
        {
            //Break
            nowWave++;
            Debug.Log("Now wave is " + nowWave);
            yield return new WaitForSeconds(breakTime);
            //Wave
            if (enemiesToSpawn + 5 < maxEnemiesToSpawn)
                enemiesToSpawn += 5;
            StartCoroutine(EnemiesSpawn(enemiesToSpawn));

            yield return new WaitForSeconds(waveTime);
        }
    }

    IEnumerator EnemiesSpawn(int countToSpawn)
    {
        int enemiesSpawned = 0;

        while (enemiesSpawned < countToSpawn)
        {
            SpawnEnemy();
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = new Vector2(WorldBounds.Instance.b_defaultMax.position.x, Random.Range(WorldBounds.Instance.b_defaultMax.position.y, WorldBounds.Instance.b_defaultMin.position.y));
        GameObject.Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation, enemiesParent);
    }
}