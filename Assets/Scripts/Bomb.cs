using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosionObjectPref;
    private Transform bombPoolTransform;

    // Start is called before the first frame update
    void Start()
    {
        bombPoolTransform = GameObject.Find("DamagingObjectsPool").transform;
        Invoke("Explosion", 3f);
        //StartCoroutine(ExplosionTask());
    }

    IEnumerator ExplosionTask()
    {
        yield return new WaitForSeconds(3);
        Explosion();
    }

    private void Explosion()
    {
        Debug.Log("BOOM!!");
        var collisions = Physics.OverlapSphere(transform.position, 3f);
        foreach(var col in collisions)
        {
            if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyPatrol")
            {
                col.GetComponent<Rigidbody>().isKinematic = false;
                col.GetComponent<Rigidbody>().freezeRotation = false;
                col.GetComponent<Rigidbody>().AddExplosionForce(600f, transform.position, 5f, 600f, ForceMode.Impulse);
                col.GetComponent<ITakeExplosionDamage>().DeadByExplosion();
            }
            if (col.gameObject.tag == "Turret")
            {
                col.GetComponent<ITakeExplosionDamage>().DeadByExplosion();
            }
            if (col.gameObject.tag == "Boss")
            {
                col.GetComponent<ITakeDamage>().TakeDamage(5);
            }
            if (col.gameObject.tag == "Player")
            {
                col.GetComponent<ITakeDamage>().TakeDamage(10);
            }
        }

        Instantiate(explosionObjectPref, transform.position, transform.rotation);
        
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        gameObject.SetActive(false);
        transform.SetParent(bombPoolTransform);

        //if (!RotPool)
        //{
        //    Destroy(gameObject);
        //}
    }
}