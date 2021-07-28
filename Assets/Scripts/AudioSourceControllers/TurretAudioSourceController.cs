using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAudioSourceController : AudioSourceController
{
    [SerializeField] private AudioClip shootSound;

    private void Awake()
    {
        Init();
        audioSource.clip = shootSound;
    }

    public void StartAudio()
    {
        if (!audioSource.isPlaying) audioSource.Play();
    }

    public void StopAudio()
    {
        if (audioSource.isPlaying) audioSource.Stop();
    }
}
