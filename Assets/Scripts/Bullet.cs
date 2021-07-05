using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Bullet : MonoBehaviour
{
    private BulletPool bulletPool;
    private Transform bulletPoolTransform;

    private void Start()
    {
        bulletPool = GameObject.Find("GameManager").GetComponent<GameManager>().BulletPool;
        bulletPoolTransform = GameObject.Find("BulletPool").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(transform.forward * /*_speed **/ Time.deltaTime);

        //transform.position = Vector3.MoveTowards(transform.position, _enemyPos.position, _speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) other.GetComponent<ITakeDamage>().TakeDamage(5);

        ReturnToPool();

        //Destroy(gameObject);

        //if (!other.CompareTag("Player") && !other.CompareTag("Turret")) Destroy(gameObject);
    }

    private void ReturnToPool()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        gameObject.SetActive(false);
        transform.SetParent(bulletPoolTransform);

        //if (!RotPool)
        //{
        //    Destroy(gameObject);
        //}
    }
}