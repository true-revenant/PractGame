using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class BulletPool
{
    private readonly HashSet<GameObject> _bullets;
    private readonly int _capacity;

    private Transform poolTransform;

    public int Capacity
    {
        get { return _bullets.Count; }
    }

    public BulletPool(int capacityPool)
    {
        _bullets = new HashSet<GameObject>();
        _capacity = capacityPool;

        if (!poolTransform)
        {
            poolTransform = new GameObject("BulletPool").transform;
            poolTransform.position = Vector3.down;
        }
    }

    public GameObject GetBullet()
    {
        var bullet = _bullets.FirstOrDefault(x => !x.gameObject.activeSelf);
        
        if (bullet == null)
        {
            var bulletPref = (GameObject)Resources.Load("Prefabs/Bullet");
            
            for (int i = 0; i < _capacity; i++)
            {
                var instBulletObj = GameObject.Instantiate(bulletPref);

                PutToPool(instBulletObj.transform);
                _bullets.Add(instBulletObj);
            }

            bullet = _bullets.FirstOrDefault(x => !x.gameObject.activeSelf);
            bullet.SetActive(true);
            return bullet;

            //GetBullet();
        }
        else
        {
            bullet.SetActive(true);
            return bullet;
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
