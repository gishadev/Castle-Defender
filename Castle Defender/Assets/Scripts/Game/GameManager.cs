using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion

    public bool isRespawning;

    public GameObject playerGO;
    public float playerRespawningTime;
    [HideInInspector] public float nowPlayerRespawningTime;
    public Transform playerRespawningSpot;

    [HideInInspector] public WavesManager waves;

    void Update()
    {
        if (isRespawning)
        {
            if (nowPlayerRespawningTime > 0)
                nowPlayerRespawningTime -= Time.deltaTime;
            else
            {
                isRespawning = false;
                PlayerRespawn();
            }   
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        UIManager.Instance.loseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void StartRespawning()
    {
        playerGO.transform.position = playerRespawningSpot.position;

        nowPlayerRespawningTime = playerRespawningTime;
        isRespawning = true;

        UIManager.Instance.ActivateRespawningMenu();
    }

    public void PlayerRespawn()
    {
        PlayerController.Instance.nowHealth = PlayerController.Instance.health;
        UIManager.Instance.UpdatePlayerHealthSlider();

        playerGO.SetActive(true);

        UIManager.Instance.DeactivateRespawningMenu();
    }

    void Awake()
    {
        Instance = this;
        waves = FindObjectOfType<WavesManager>();
    }
}
