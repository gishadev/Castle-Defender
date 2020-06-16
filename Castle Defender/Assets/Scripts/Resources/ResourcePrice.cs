using UnityEngine;
using TMPro;

[System.Serializable]
public class ResourcePrice
{
    public ResourceData resourceData;
    public int price;
}

[System.Serializable]
public class ResourceCount
{
    public ResourceData resourceData;
    public int count;

    public TMP_Text UIText;
}
