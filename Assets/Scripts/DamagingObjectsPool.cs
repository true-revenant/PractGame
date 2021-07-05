using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class DamagingObjectsPool
{
    //private readonly HashSet<GameObject> _bullets;
    //private readonly HashSet<GameObject> _bombs;

    private readonly Dictionary<string, HashSet<GameObject>> _damagingObjectsPool;

    private readonly int _capacity;

    private Transform poolTransform;

    public int BulletsCapacity 
    { 
        get { return GetDamagingObjectsList(DamagingObjectType.Bullet).Count; }
    }
    public int BombsCapacity
    {
        get { return GetDamagingObjectsList(DamagingObjectType.Bomb).Count; }
    }

    public DamagingObjectsPool(int capacityPool)
    {
        //_bullets = new HashSet<GameObject>();
        //_bombs = new HashSet<GameObject>();

        _damagingObjectsPool = new Dictionary<string, HashSet<GameObject>>();

        _capacity = capacityPool;

        if (!poolTransform)
        {
            poolTransform = new GameObject("DamagingObjectsPool").transform;
            poolTransform.position = Vector3.down;
        }
    }

    private void InstDamagingObj(GameObject _pref, DamagingObjectType damagingObjType)
    {
        var instDamagingObj = GameObject.Instantiate(_pref);
        PutToPool(instDamagingObj.transform);
        GetDamagingObjectsList(damagingObjType).Add(instDamagingObj);

    }

    private HashSet<GameObject> GetDamagingObjectsList(DamagingObjectType damagingObjectType)
    {
        return _damagingObjectsPool.ContainsKey(damagingObjectType.ToString()) ? _damagingObjectsPool[damagingObjectType.ToString()] : 
            _damagingObjectsPool[damagingObjectType.ToString()] = new HashSet<GameObject>();
    }

    public GameObject GetDamagingObject(DamagingObjectType damagingObjectType)
    {
        //var damagingObj = _bullets.FirstOrDefault(x => !x.gameObject.activeSelf);

        var damagingObj = GetDamagingObjectsList(damagingObjectType).FirstOrDefault(x => !x.gameObject.activeSelf);

        if (damagingObj == null)
        {
            var damagingObjPref = (GameObject)Resources.Load("Prefabs/" + damagingObjectType.ToString());

            for (int i = 0; i < _capacity; i++)
            {
                InstDamagingObj(damagingObjPref, damagingObjectType);
            }
            
            //damagingObj = _bullets.FirstOrDefault(x => !x.gameObject.activeSelf);
            damagingObj = GetDamagingObjectsList(damagingObjectType).FirstOrDefault(x => !x.gameObject.activeSelf);

            damagingObj.SetActive(true);
            return damagingObj;
        }
        else
        {
            damagingObj.SetActive(true);
            return damagingObj;
        }
    }

    public void RemovePool()
    {
        Object.Destroy(poolTransform.gameObject);
    }

    private void PutToPool(Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.gameObject.SetActive(false);
        transform.SetParent(poolTransform);
    }
}

public enum DamagingObjectType
{
    Bullet,
    Bomb
}