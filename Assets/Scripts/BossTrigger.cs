using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private Door bossDoor;
    [SerializeField] private GameObject healthLineObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossDoor.doorIsOpening = true;
            healthLineObj.SetActive(true);
        }
    }
}
