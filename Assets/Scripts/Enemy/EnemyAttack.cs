using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, IAttack
{
    [SerializeField] protected Transform bulletStartPos;

    protected BulletPool bulletPool;
    protected Transform bulletPoolTransform;

    private void Start()
    {
        InitBulletPool();
    }

    protected void InitBulletPool()
    {
        bulletPool = GameObject.Find("GameManager").GetComponent<GameManager>().BulletPool;
        bulletPoolTransform = GameObject.Find("BulletPool").transform;
    }

    public void CreateRaycastBullet()
    {
        var bullet = bulletPool.GetBullet();
        bullet.transform.position = bulletStartPos.position;
        bullet.transform.rotation = Quaternion.identity;
        var rBody = bullet.GetComponent<Rigidbody>();

        rBody.velocity = bulletStartPos.forward * 15f;

        Debug.Log($"BULLETS IN POOL = {bulletPool.Capacity}");
        Debug.Log($"BULLET CHILDS IN POOL TRANSFORM = {bulletPoolTransform.childCount}");
    }
}
