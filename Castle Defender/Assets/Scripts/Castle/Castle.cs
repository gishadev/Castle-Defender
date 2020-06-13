using UnityEngine;

public class Castle : MonoBehaviour, IDamageable
{
    #region Singleton
    public static Castle Instance;
    #endregion

    public GameObject outerPart;
    public GameObject innerPart;

    [SerializeField] bool playerInside;

    void Awake()
    {
        Instance = this;
    }

    public void UpdateVisibility(bool isInside)
    {
        playerInside = isInside;

        if (playerInside)
        {
            outerPart.SetActive(false);
            innerPart.SetActive(true);
        }

        else
        {
            outerPart.SetActive(true);
            innerPart.SetActive(false);
        }

    }

    public void GetDamage(int dmg)
    {

    }

    //outer part disappears when triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerFoots"))
            UpdateVisibility(true);
    }

    //outer part appears when triggers
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerFoots"))
            UpdateVisibility(false);
    }
}
