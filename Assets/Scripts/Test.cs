using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //[SerializeField] private Transform target;
    //[SerializeField] private float speed;

    int sign = 1;

    // Update is called once per frame
    void Update()
    {
        //InititalRotation();

        //Vector3 relative = target.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relative);
        //transform.rotation = rotation;

        //Vector3 relative = target.position - transform.position;
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, relative, speed * Time.deltaTime, 0f);
        //transform.rotation = Quaternion.LookRotation(newDir);

    }

    private void Start()
    {
        Debug.Log(transform.rotation);
    }

    private void InititalRotation()
    {
        // changing direction
        if (transform.rotation.y >= 0.9) sign = -sign;

        transform.Rotate(Vector3.up * Time.deltaTime * 25 * sign);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter()");
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("OnCollisionStay()");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit()");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter()");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay()");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit()");
    }
}
