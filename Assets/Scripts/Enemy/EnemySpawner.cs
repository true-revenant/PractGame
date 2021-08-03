using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class EnemySpawner : MonoBehaviour, IEnemyFactory
{
    public GameObject _enemyPrefab;
    public int _count = 10;
    public Transform _spawnerTransform;
    public float _radius = 1f;

    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform parent;

    public GameObject CreateEnemyObject()
    {
        var rndDelta = Random.insideUnitSphere * _radius;

        /////
        //// DeepCopy , Prototype pattern
        //// не работает, так как тип GameObject не является сериализуемым
        /////
        
        //var enemy = _enemyPrefab.DeepCopy();
        //enemy.transform.position = new Vector3(_spawnerTransform.position.x + rndDelta.x, 0, _spawnerTransform.position.z + rndDelta.z);
        //enemy.transform.rotation = Quaternion.identity;
        //enemy.transform.parent = parent;
        //enemy.GetComponent<EnemyBehave>().Init(playerPos);

        var enemy = Instantiate(_enemyPrefab, new Vector3(_spawnerTransform.position.x + rndDelta.x, 0, _spawnerTransform.position.z + rndDelta.z), Quaternion.identity, parent).GetComponent<EnemyBehave>();
        enemy.Init(playerPos);
        
        return enemy.gameObject;
    }

    public void Spawn()
    {
        for (int i = 0; i < _count; i++)
        {
            CreateEnemyObject();

            //var rndDelta = Random.insideUnitSphere * _radius;
            //var enemy = Instantiate(_enemyPrefab, new Vector3(_spawnerTransform.position.x + rndDelta.x, 0, _spawnerTransform.position.z + rndDelta.z), Quaternion.identity, parent).GetComponent<EnemyBehave>();
            //enemy.Init(playerPos);
        }
    }
}
