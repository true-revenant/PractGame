using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;

    public void Init(Transform enemyPos) {}

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * _speed * Time.deltaTime);

        //transform.position = Vector3.MoveTowards(transform.position, _enemyPos.position, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) other.GetComponent<Enemy>().TakeDamage();
        
        if (!other.CompareTag("Player")) Destroy(gameObject);
    }
}
