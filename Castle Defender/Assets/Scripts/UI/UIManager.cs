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
