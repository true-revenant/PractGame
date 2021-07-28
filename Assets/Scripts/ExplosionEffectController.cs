using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectController : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private AudioSource _audioSource;
    private SoundController soundController;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();

        soundController = GameObject.Find("SoundManager").GetComponent<SoundController>();
        _audioSource.volume = soundController.EffectsVolume;
    }

    private void Start()
    {
        _particleSystem.Play();
        _audioSource.Play();
        Destroy(gameObject, 3f);
    }
}
