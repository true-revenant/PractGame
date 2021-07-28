using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAudioSourceController : AudioSourceController
{
    [SerializeField] private AudioClip shootSound;

    private void Awake()
    {
        Init();
    }

    public void PlayAudio()
    {
        audioSource.PlayOneShot(shootSound);
    }
}
