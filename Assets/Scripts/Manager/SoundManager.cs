using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        RemoveAllAudioSource();
        Intialize();
    }
    public void Intialize()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.clip = sounds[i].audioClip;
            sounds[i].source.loop = sounds[i].loop;
            sounds[i].source.pitch = sounds[i].pitch;
            //sounds[i].source.mute = state;
        }
    }
    public void RemoveAllAudioSource()
    {
        foreach(AudioSource s in GetComponents<AudioSource>())
        {
            Destroy(s);
        }
    }
    public void IntializeBgmSoundsAsState(bool state)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].soundType == SoundType.BGM)
            {
                //sounds[i].source = gameObject.AddComponent<AudioSource>();
                sounds[i].source.volume = sounds[i].volume;
                sounds[i].source.clip = sounds[i].audioClip;
                sounds[i].source.loop = sounds[i].loop;
                sounds[i].source.pitch = sounds[i].pitch;
                sounds[i].source.mute = state;
            }
        }
    }
    public void IntializeSfxSoundsAsState(bool state)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].soundType == SoundType.SFX)
            {
                //sounds[i].source = gameObject.AddComponent<AudioSource>();
                sounds[i].source.volume = sounds[i].volume;
                sounds[i].source.clip = sounds[i].audioClip;
                sounds[i].source.loop = sounds[i].loop;
                sounds[i].source.pitch = sounds[i].pitch;
                sounds[i].source.mute = state;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound(string name)
    {
        Sound sound = Array.Find(sounds, (x) => x.name.Equals(name));
        if (sound == null)
        {
            Debug.LogError("Coudn't find sound : " + sound);
            return;
        }
        sound.source.Play();
    }
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;

    public SoundType soundType;

    [Range(0f,1f)]
    public float volume;

    [Range(0f, 1f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
    public bool loop;
}
public enum SoundType
{
    BGM,
    SFX
}
