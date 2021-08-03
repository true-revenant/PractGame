using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour, IBombAttack
{
    [SerializeField] private GameObject bombPref;
    [SerializeField] private Transform bombStartPos;
    [SerializeField] private float force;

    public void CreateBomb()
    {
        var rBody = Instantiate(bombPref, bombStartPos.position, transform.rotation).GetComponent<Rigidbody>();
        rBody.AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
