using UnityEngine;
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

    [HideInInspector] public InteractTarget nowInteractTarget;

    [HideInInspector] public Inventory inventory;
    [HideInInspector] public Hotbar hotbar;
    [HideInInspector] public Crafting crafting;
    void Awake()
    {
        Instance = this;

        inventory = FindObjectOfType<Inventory>();
        hotbar = FindObjectOfType<Hotbar>();
        crafting = FindObjectOfType<Crafting>();
    }

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
