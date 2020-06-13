using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    #region Singleton
    public static PlayerController Instance;
    #endregion
    public int health;

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
            direction *= new Vector2(-1f,1f);
        }

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        zRotatorTrans.localRotation = Quaternion.Euler(transform.forward * Mathf.Clamp(rotZ, minZ, maxZ));
    }
    void TurnPlayer(float x)
    {
        transform.localScale = new Vector2(x, 1f);
    }

    public void TakeGear(InventoryGear invGear)
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
        health -= dmg;

        if (health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
