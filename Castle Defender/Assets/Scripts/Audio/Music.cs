using UnityEngine;

public class Music : MonoBehaviour
{
    #region Singleton
    public static Music Instance { get; private set; }
    #endregion

    public AudioClip dayMusic;
    public AudioClip nightMusic;

    AudioSource source;
    Animator animator;

    void Awake()
    {
        Instance = this;

        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void StartFade()
    {
        animator.SetTrigger("Fade");
    }

    public void ChangeTheme()
    {
        if (GameManager.Instance.waves.isWave)
            source.clip = nightMusic;
        else source.clip = dayMusic;

        source.Play();
    }
}
