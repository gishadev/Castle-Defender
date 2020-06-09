using UnityEngine;
using UnityEngine.Rendering;

public class DynamicSortingGroup : MonoBehaviour
{
    public bool isStationary;

    public Transform customPivot;

    bool isCustomPivot;
    SortingGroup sortingGroup;

    void Start()
    {
        sortingGroup = GetComponent<SortingGroup>();

        isCustomPivot = customPivot != null;

        if (!isCustomPivot)
            sortingGroup.sortingOrder = (int)(-transform.position.y * 10f);
        else
            sortingGroup.sortingOrder = (int)(-customPivot.position.y * 10f);
    }

    void Update()
    {
        if (!isStationary)
        {
            if (!isCustomPivot)
                sortingGroup.sortingOrder = (int)(-transform.position.y * 10f);
            else
                sortingGroup.sortingOrder = (int)(-customPivot.position.y * 10f);
        }
    }
}
