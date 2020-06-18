using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance;
    #endregion

    [Space]
    public GameObject pressToInteractBtn;
    [Space]
    public ColliderUIBounds interactBounds;
    public ColliderUIBounds miningBounds;

    [Header("Player")]
    public Slider playerHealth;
    [Space]
    public GameObject respawningMenu;
    public Image respawningCircleImg;
    public TMP_Text respawningTime;

    [Space]
    public Slider castleHealth;
    [Space]
    [Header("Game Timer")]
    public TMP_Text waveCountText;
    [Space]
    public Slider timer;
    public Image timerFill;
    public TMP_Text gameTimeText;

    public Color breakTimerColor;
    public Color waveTimerColor;

    [Tooltip("Icon that represents current wave or break")]
    public Image gameTimeIcon;

    public Color breakIconColor;
    public Color waveIconColor;

    [HideInInspector] public InteractTarget nowInteractTarget;

    [HideInInspector] public Inventory inventory;
    [HideInInspector] public Hotbar hotbar;
    [HideInInspector] public Crafting crafting;

    Camera cam;
    void Awake()
    {
        Instance = this;

        inventory = FindObjectOfType<Inventory>();
        hotbar = FindObjectOfType<Hotbar>();
        crafting = FindObjectOfType<Crafting>();

        cam = Camera.main;
    }

    void Update()
    {
        //Health bar follows player
        PivotPlayerHealth();
        PivotCastleHealth();

        if (GameManager.Instance.isRespawning)
        {
            UpdateRespawningInfo();
        }
    }
    #region Player
    void PivotPlayerHealth()
    {
        Vector2 screenPos = cam.WorldToScreenPoint(PlayerController.Instance.transform.position);
        playerHealth.transform.position = screenPos + Vector2.up * -80f;
    }

    public void UpdatePlayerHealthSlider()
    {
        playerHealth.value = (float)PlayerController.Instance.nowHealth / (float)PlayerController.Instance.health;

        if (playerHealth.value == 0)
            playerHealth.gameObject.SetActive(false);
        else
            playerHealth.gameObject.SetActive(true);
    }

    public void ActivateRespawningMenu()
    {
        respawningMenu.SetActive(true);
    }

    public void UpdateRespawningInfo()
    {
        respawningCircleImg.fillAmount = GameManager.Instance.nowPlayerRespawningTime / GameManager.Instance.playerRespawningTime;
        respawningTime.text = Mathf.Round(GameManager.Instance.nowPlayerRespawningTime).ToString();
    }

    public void DeactivateRespawningMenu()
    {
        respawningMenu.SetActive(false);
    }
    #endregion

    #region Castle
    void PivotCastleHealth()
    {
        Vector2 screenPos = cam.WorldToScreenPoint(Castle.Instance.healthbarPos.position);
        castleHealth.transform.position = screenPos + Vector2.up * -80f;
    }

    public void UpdateCastleHealthSlider()
    {
        castleHealth.value = (float)Castle.Instance.nowHealth / (float)Castle.Instance.health;

        if (castleHealth.value == 0)
            castleHealth.gameObject.SetActive(false);
    }
    #endregion

    #region Waves
    public IEnumerator WaveTimer()
    {
        float time = GameManager.Instance.waves.waveTime + 1;
        timerFill.color = waveTimerColor;
        gameTimeIcon.color = waveIconColor;

        while (GameManager.Instance.waves.isWave)
        {
            time--;
            gameTimeText.text = time.ToString();

            timer.value = time / (GameManager.Instance.waves.waveTime);

            yield return new WaitForSeconds(1.0f);
        }
    }

    public IEnumerator BreakTimer()
    {
        float time = GameManager.Instance.waves.breakTime + 1;
        timerFill.color = breakTimerColor;
        gameTimeIcon.color = breakIconColor;

        while (!GameManager.Instance.waves.isWave)
        {
            time--;
            gameTimeText.text = time.ToString();

            timer.value = time / (GameManager.Instance.waves.breakTime);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void ChangeWaveCount(int _waveCount)
    {
        waveCountText.text = _waveCount.ToString();
    }
    #endregion

    #region Interactable
    public void ShowInteractable(Bounds _bounds)
    {
        pressToInteractBtn.transform.position = Camera.main.WorldToScreenPoint(new Vector2(_bounds.center.x, _bounds.max.y + 0.5f));

        interactBounds.CreateBounds(_bounds);
        pressToInteractBtn.SetActive(true);
    }

    public void HideInteractable()
    {
        interactBounds.ClearBounds();
        pressToInteractBtn.SetActive(false);
    }
    #endregion 

    public void UpdateResourcesUIData(ResourceCount[] resCount)
    {
        for (int i = 0; i < resCount.Length; i++)
        {
            resCount[i].UIText.text = resCount[i].count.ToString();
        }
    }
}

[System.Serializable]
public class ColliderUIBounds
{
    public GameObject boundsGO;
    [Header("Points")]
    public Transform rt;
    public Transform lt;
    public Transform rb;
    public Transform lb;

    public void CreateBounds(Bounds bounds)
    {
        rt.position = Camera.main.WorldToScreenPoint(new Vector2(bounds.max.x, bounds.max.y));
        lt.position = Camera.main.WorldToScreenPoint(new Vector2(bounds.min.x, bounds.max.y));
        rb.position = Camera.main.WorldToScreenPoint(new Vector2(bounds.max.x, bounds.min.y));
        lb.position = Camera.main.WorldToScreenPoint(new Vector2(bounds.min.x, bounds.min.y));

        boundsGO.SetActive(true);
    }

    public void ClearBounds()
    {
        boundsGO.SetActive(false);
    }
}
