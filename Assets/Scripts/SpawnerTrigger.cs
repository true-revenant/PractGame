using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrigger : MonoBehaviour
{
    public GameObject[] spawners;

    private bool spawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !spawned)
        {
            for (int i = 0; i < spawners.Length; i++)
                spawners[i].GetComponent<EnemySpawner>().Spawn();

            spawned = true;
        }
    }
}
