using UnityEngine;

public class Castle : MonoBehaviour, IDamageable
{
    #region Singleton
    public static Castle Instance { get; private set; }
    #endregion

    public Transform healthbarPos;

    public int health;
    [HideInInspector] public int nowHealth;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        nowHealth = health;
    }

    public void GetDamage(int dmg)
    {
        nowHealth -= dmg;
        AudioManager.Instance.Play("Castle_GetHit");

        UIManager.Instance.UpdateCastleHealthSlider();

        if (nowHealth <= 0)
            DestroyCastle();
    }

    void DestroyCastle()
    {
        GameManager.Instance.EndGame();
    }
}
