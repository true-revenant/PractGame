using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class SoundController : MonoBehaviour
{
    public float EffectsVolume { get; set; } = 1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("BackgroundMusic"));
    }
}
