using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BulletAttack : MonoBehaviour, IAttack
{
    [SerializeField] protected Transform bulletStartPos;

    protected DamagingObjectsPool bulletPool;
    protected Transform bulletPoolTransform;

    private BulletAudioSourceController bulletAudioSourceController;

    private void Awake()
    {
        bulletAudioSourceController = GetComponent<BulletAudioSourceController>();
    }

    private void Start()
    {
        InitBulletPool();
    }

    protected void InitBulletPool()
    {
        bulletPool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().BulletPool;
        bulletPoolTransform = GameObject.FindGameObjectWithTag("Pool").transform;
    }

    public void CreateBullet()
    {
        if (Time.timeScale == 1)
        {
            if (bulletAudioSourceController != null)
                bulletAudioSourceController.PlayAudio();

            var bullet = bulletPool.GetDamagingObject(DamagingObjectType.Bullet);
            bullet.transform.position = bulletStartPos.position;
            bullet.transform.rotation = Quaternion.identity;
            var rBody = bullet.GetComponent<Rigidbody>();

            rBody.velocity = bulletStartPos.forward * 15f;

            //Debug.Log($"BULLETS IN POOL = {bulletPool.BulletsCapacity}");
            //Debug.Log($"BULLET CHILDS IN POOL TRANSFORM = {bulletPoolTransform.childCount}");
        }
    }
}
