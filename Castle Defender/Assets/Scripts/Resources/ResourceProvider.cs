using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProvider : MonoBehaviour
{
    public string resourceTargetName;

    [Header("Supply Counts")]
    public int minSupplyPerHit;
    public int maxSupplyPerHit;

    public int minFinalSupply;
    public int maxFinalSupply;
}
