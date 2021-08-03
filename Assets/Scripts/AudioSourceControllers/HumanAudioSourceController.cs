using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class HumanAudioSourceController : AudioSourceController
{
    [SerializeField] AudioClip[] stepsSounds;
    [SerializeField] AudioClip jumpSound;

    private void Awake()
    {
        Init();
    }

    public void PlayStepAudio()
    {
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(stepsSounds[Random.Range(0, stepsSounds.Length - 1)]);
    }

    public void PlayJumpAudio()
    {
        if (!audioSource.isPlaying) audioSource.PlayOneShot(jumpSound);
    }

    public void StopAudio()
    {
        if (audioSource.isPlaying) audioSource.Stop();
    }
}
