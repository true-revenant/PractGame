using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var e in enemies)
        {
            e.GetComponent<Enemy>().TakeDamage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(float time)
    {
        Destroy(gameObject, time);
    }
}
