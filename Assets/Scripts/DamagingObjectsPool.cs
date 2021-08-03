using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal sealed class DamagingObjectsPool
{
    private readonly Dictionary<string, HashSet<GameObject>> _damagingObjectsPool;
    private readonly int _capacity;
    private Transform _poolTransform;
    
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
        _damagingObjectsPool = new Dictionary<string, HashSet<GameObject>>();

        _capacity = capacityPool;

        if (!_poolTransform)
        {
            _poolTransform = new GameObject("DamagingObjectsPool").transform;
            _poolTransform.gameObject.tag = "Pool";
            _poolTransform.position = Vector3.down;
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
        var damagingObj = GetDamagingObjectsList(damagingObjectType).FirstOrDefault(x => !x.gameObject.activeSelf);

        if (damagingObj == null)
        {
            GameObject damagingObjPref = null;
            if (damagingObjectType == DamagingObjectType.Bullet)
            {
                ///
                // BUILDER
                ///

                //var bulletBuilder = new BulletBuilder();
                //damagingObjPref = bulletBuilder.Visual
                //                        .AddName("Bullet")
                //                        .AddRenderer(Resources.Load<Material>("Materials/PlayerMat"))
                //                        .Physics.AddRigidBody().AddScriptBullet().AddSphereCollider();

                ///
                // FLUENT BUILDER
                ///

                damagingObjPref = GameObject.CreatePrimitive(PrimitiveType.Sphere)
                                        .SetBulletScale(0.1f)
                                        .AddName("Bullet")
                                        .AddBulletScript()
                                        .AddMeshRenderer(Resources.Load<Material>("Materials/PlayerMat"))
                                        .AddRigidBody()
                                        .AddSphereCollider();

            }
            else if (damagingObjectType == DamagingObjectType.Bomb)
            {
                damagingObjPref = (GameObject)Resources.Load("Prefabs/" + damagingObjectType.ToString());
            }

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
        Object.Destroy(_poolTransform.gameObject);
    }

    private void PutToPool(Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.gameObject.SetActive(false);
        transform.SetParent(_poolTransform);
    }
}

public enum DamagingObjectType
{
    Bullet,
    Bomb
}