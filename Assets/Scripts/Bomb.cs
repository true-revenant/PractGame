using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explosion", 3f);
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
                col.GetComponent<LiveObj>().DeadByExplosion();
            }
            if (col.gameObject.tag == "Turret")
            {
                col.GetComponent<LiveObj>().DeadByExplosion();
            }
            if (col.gameObject.tag == "Boss")
            {
                col.GetComponent<LiveObj>().TakeDamage(5);
            }
            if (col.gameObject.tag == "Player")
            {
                col.GetComponent<LiveObj>().TakeDamage(10);
            }
        }
        Destroy(gameObject);
    }
}
