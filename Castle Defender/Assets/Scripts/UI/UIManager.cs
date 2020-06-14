using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance;
    #endregion

    public TMP_Text WoodText;
    public TMP_Text StoneText;
    public TMP_Text IronText;
    public TMP_Text GoldText;
    [Space]
    public GameObject pressToInteractBtn;
    [Space]
    public InteractBounds interactBounds;
    void Awake()
    {
        Instance = this;
    }

    public void UpdateResourcesUIData()
    {
        WoodText.text = ResourceManager.Instance.GetResource("Wood").count.ToString();
        StoneText.text = ResourceManager.Instance.GetResource("Stone").count.ToString();
        IronText.text = ResourceManager.Instance.GetResource("Iron").count.ToString();
        GoldText.text = ResourceManager.Instance.GetResource("Gold").count.ToString();
    }
}

[System.Serializable]
public class InteractBounds
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
