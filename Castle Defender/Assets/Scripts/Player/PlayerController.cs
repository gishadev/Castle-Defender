using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    #region Singleton
    public static PlayerController Instance;
    #endregion
    public int health;

    [HideInInspector] public int nowHealth;

    [HideInInspector] public Animator playerAnimator;

    [HideInInspector] public Gear nowGear;

    public Transform handTrans;
    [Space]
    public Transform zRotatorTrans;
    public float maxZ, minZ;

    [Space]
    float gearDelay;

    Camera cam;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        nowHealth = health;

        cam = Camera.main;
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (nowGear != null)
        {
            if (gearDelay <= 0)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    nowGear.Act();
                    gearDelay = nowGear.delay;
                }
            }
            else
                gearDelay -= Time.deltaTime;
        }

        ZRotate();
    }

    void ZRotate()
    {
        Vector2 direction = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (direction.x > 0)
            TurnPlayer(1f);
        else if (direction.x < 0)
        {
            TurnPlayer(-1f);
            direction *= new Vector2(-1f, 1f);
        }

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + nowGear.zOffset;
        zRotatorTrans.localRotation = Quaternion.Euler(transform.forward * Mathf.Clamp(rotZ, minZ, maxZ));
    }
    void TurnPlayer(float x)
    {
        transform.localScale = new Vector2(x, 1f);
    }

    public void SpawnGear(InventoryGearData invGear)
    {
        GameObject gearGO = GameObject.Instantiate(invGear.prefab, invGear.spawnPosition, Quaternion.identity, handTrans);
        gearGO.transform.localPosition = invGear.spawnPosition;
        gearGO.transform.localRotation = Quaternion.Euler(Vector3.zero);

        nowGear = gearGO.GetComponent<Gear>();

        gearDelay = nowGear.delay;
    }

    public void DeleteOldGear()
    {
        if (nowGear != null)                 // Deleting an Old Gear
            Destroy(nowGear.gameObject);    //
    }

    public void GetDamage(int dmg)
    {
        nowHealth -= dmg;

        UIManager.Instance.UpdatePlayerHealthSlider();

        if (nowHealth <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
