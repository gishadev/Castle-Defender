using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance { get; private set; }
    #endregion

    public Sound[] allSounds;

    void Awake()
    {
        Instance = this;

        //Setting up audio source variables
        foreach (Sound s in allSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string _name)
    {
        Sound s = Array.Find(allSounds, x => x.soundName == _name);

        if (s == null)
        {
            Debug.LogError("Undefined sound name");
            return;
        }

        
        s.pitch += UnityEngine.Random.Range(-0.1f,0.1f);
        s.source.Play();
    }
}

[System.Serializable]
public class Sound
{
    public string soundName;
    [Space]
    public AudioClip clip;
    [Space]
    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;

    [HideInInspector] public AudioSource source;
}
