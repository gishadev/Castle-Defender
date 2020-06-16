using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftingUIItem : MonoBehaviour
{
    public TMP_Text nameText;
    public Image previewImg;
    [Header("Prices")]
    public TMP_Text WoodText;
    public TMP_Text StoneText;
    public TMP_Text IronText;
    public TMP_Text GoldText;

    Button btn;
    public void SetUpItemData(BlueprintData bp_Data)
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate { UIManager.Instance.crafting.OnCraftBtn(bp_Data); });

        nameText.text = bp_Data.gearData.m_name;
        previewImg.sprite = bp_Data.gearData.inventorySprite;

        for (int i = 0; i < bp_Data.resourcePrices.Length; i++)
        {
            switch (bp_Data.resourcePrices[i].resourceData.m_name)
            {
                case "Wood":
                    WoodText.text = bp_Data.resourcePrices[i].price.ToString();
                    break;

                case "Stone":
                    StoneText.text = bp_Data.resourcePrices[i].price.ToString();
                    break;

                case "Iron":
                    IronText.text = bp_Data.resourcePrices[i].price.ToString();
                    break;

                case "Gold":
                    GoldText.text = bp_Data.resourcePrices[i].price.ToString();
                    break;
            }
        }
    }
}
