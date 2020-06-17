using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion

    [HideInInspector] public WavesManager waves;


    void Awake()
    {
        Instance = this;
        waves = FindObjectOfType<WavesManager>();
    }
}
