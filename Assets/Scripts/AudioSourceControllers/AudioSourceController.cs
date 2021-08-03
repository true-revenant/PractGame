using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class AudioSourceController : MonoBehaviour
{
    protected AudioSource audioSource;
    protected SoundController soundController;

    protected void Init()
    {
        audioSource = GetComponent<AudioSource>();
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();
        audioSource.volume = soundController.EffectsVolume;
    }
}
