using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    #region Singleton
    public static CameraShaker Instance { get; private set; }
    #endregion

    private Animator animator;

     void Awake()
    {
        Instance = this;    
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Shake()
    {
        int n = Random.Range(1, 4);

        animator.SetTrigger("shake" + n.ToString());
    }
}
