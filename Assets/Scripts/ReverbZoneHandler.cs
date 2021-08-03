using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class ReverbZoneHandler : MonoBehaviour
{
    private AudioReverbZone reverbZone;

    private void Awake()
    {
        reverbZone = GetComponent<AudioReverbZone>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            reverbZone.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            reverbZone.enabled = false;
    }
}
