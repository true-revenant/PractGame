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

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(transform.forward * Time.deltaTime * 5f);
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
                //col.GetComponent<Rigidbody>().isKinematic = true;
                col.GetComponent<LiveObj>().TakeDamage(50);
            }
        }
        Destroy(gameObject);
    }
}
