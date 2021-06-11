using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject _enemyPrefab;
    public int _count = 10;
    public Transform _spawnerTransform;
    public float _radius = 1f;

    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform parent;
    
    public void Spawn()
    {

        for (int i = 0; i < _count; i++)
        {
            var rndDelta = Random.insideUnitSphere * _radius;
            var enemy = Instantiate(_enemyPrefab, new Vector3(_spawnerTransform.position.x + rndDelta.x, 0, _spawnerTransform.position.z + rndDelta.z), Quaternion.identity, parent).GetComponent<Enemy>();
            enemy.Init(playerPos);
        }
    }
}
