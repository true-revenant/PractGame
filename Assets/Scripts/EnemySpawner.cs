using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject _enemyPrefab;
    public int _count = 10;
    public Transform _spawnerTransform;
    public float _radius = 1f;
    
    // Start is called before the first frame update
    private void Start()
    {
        //SpawnEnemies();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Spawn()
    {

        for (int i = 0; i < _count; i++)
        {
            var rndDelta = Random.insideUnitSphere * _radius;
            Instantiate(_enemyPrefab, new Vector3(_spawnerTransform.position.x + rndDelta.x, 0, _spawnerTransform.position.z + rndDelta.z), Quaternion.identity);
        }
    }
}
