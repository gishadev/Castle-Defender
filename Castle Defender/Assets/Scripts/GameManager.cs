using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion

    public Transform castleTrans;
    [Space]
    public int nowWave;
    public WavesManager wavesManager;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(wavesManager.Game());
    }
}

public class WavesManager
{
    public float waveTime;
    public float breakTime;
    [Space]
    public bool enemySpawning;
    public float spawnRate;

    private int enemiesToSpawn = 5;
    private int maxEnemiesToSpawn = 100;

    public IEnumerator Game()
    {
        while (true)
        {
            //Break

            yield return new WaitForSeconds(breakTime);

            //Wave
            
            yield return new WaitForSeconds(waveTime); 
        }
    }

    //IEnumerator EnemiesSpawn(int count)
    //{
    //    while (enemySpawning)
    //    {

    //    }
    //}
}
