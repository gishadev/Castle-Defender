using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion

    public Transform castleTrans;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    }
}
