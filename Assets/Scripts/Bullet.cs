using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Bullet : MonoBehaviour
{
    private Transform bulletPoolTransform;

    private void Start()
    {
        bulletPoolTransform = GameObject.Find("DamagingObjectsPool").transform;
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