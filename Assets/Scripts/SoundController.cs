using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static float _musicVolume;
    private static float _effectsVolume;

    //private AudioSource backMusicAudioSource;

    public static float MusicVolume
    {
        get { return _musicVolume; }
        set
        {
            AudioSource backMusicAudioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
            backMusicAudioSource.volume = value;
        }
    }
    //public static float EffectsVolume { get; set; }

    private void Awake()
    {
        //backMusicAudioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("BackgroundMusic"));
    }

    public void SetMusicVolume(float value)
    {

    }
}
