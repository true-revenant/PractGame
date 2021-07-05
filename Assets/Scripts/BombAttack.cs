using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttack : MonoBehaviour, IBombAttack
{
    //[SerializeField] private GameObject bombPref;
    [SerializeField] protected Transform bombStartPos;
    [SerializeField] protected float force;

    protected DamagingObjectsPool bombPool;
    protected Transform bombPoolTransform;

    private void Start()
    {
        InitBombPool();
    }

    protected void InitBombPool()
    {
        bombPool = GameObject.Find("GameManager").GetComponent<GameManager>().BulletPool;
        bombPoolTransform = GameObject.Find("DamagingObjectsPool").transform;
    }

    public void CreateBomb()
    {
        //var rBody = Instantiate(bombPref, bombStartPos.position, transform.rotation).GetComponent<Rigidbody>();
        //rBody.AddForce(transform.forward * force, ForceMode.Impulse);

        var bomb = bombPool.GetDamagingObject(DamagingObjectType.Bomb);
        bomb.transform.position = bombStartPos.position;
        bomb.transform.rotation = Quaternion.identity;
        var rBody = bomb.GetComponent<Rigidbody>();

        rBody.AddForce(transform.forward * force, ForceMode.Impulse);

        Debug.Log($"BULLETS IN POOL = {bombPool.BulletsCapacity}");
        Debug.Log($"BULLET CHILDS IN POOL TRANSFORM = {bombPoolTransform.childCount}");
    }
}
