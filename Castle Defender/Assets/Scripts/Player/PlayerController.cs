using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController Instance;
    #endregion

    [HideInInspector] public Animator playerAnimator;

    [HideInInspector] public Gear nowGear;

    public Transform handTrans;

    float gearDelay;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
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
}
