using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] private float _speed = 4f;

    public void Init() {}

    // Start is called before the first frame update
    private void Start() {}

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(transform.forward * /*_speed **/ Time.deltaTime);

        //transform.position = Vector3.MoveTowards(transform.position, _enemyPos.position, _speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) other.GetComponent<ITakeDamage>().TakeDamage(5);

        Destroy(gameObject);
        //if (!other.CompareTag("Player") && !other.CompareTag("Turret")) Destroy(gameObject);
    }
}