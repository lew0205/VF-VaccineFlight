using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource BGMSound;
    public AudioClip BGM1;
    public AudioClip BGM2;

    private void Start()
    {
        BGMSound = GetComponent<AudioSource>();
        PlayBGM(BGM1);

    }

    public void PlaySoundEffect(string VFXname, AudioClip clip,float sound)
    {
        GameObject soundEffect = new GameObject(VFXname + "SoundEffect");
        Debug.Log(soundEffect);
        AudioSource audioSource = soundEffect.gameObject.AddComponent<AudioSource>();
        Debug.Log(audioSource);
        audioSource.clip = clip;
        audioSource.volume = sound;
        audioSource.Play();

        Destroy(soundEffect, clip.length);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (BGMSound.isPlaying)
            BGMSound.Stop();
        BGMSound.clip = clip;
        BGMSound.loop = true;
        BGMSound.volume = .1f;
        BGMSound.Play();
    }
}
