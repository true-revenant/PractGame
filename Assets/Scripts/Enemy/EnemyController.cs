using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : LiveObj, ITakeDamage, ITakeExplosionDamage
{
    private Animator animator;

    private void Awake()
    {
        maxHP = 50;
        currentHP = maxHP;
        IsAlive = true;

        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{name} : OUCH!!!");
        currentHP -= damage;
        if (currentHP <= 0 && IsAlive)
        {
            StartCoroutine(DeathAnimation());
        }
    }

    public void DeadByExplosion()
    {
        StartCoroutine(DeathByExplosionAnimation());
    }

    IEnumerator DeathByExplosionAnimation()
    {
        IsAlive = false;
        animator.enabled = false;
        gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        //yield return null;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    IEnumerator DeathAnimation()
    {
        IsAlive = false;
        //animator.SetBool("Die", true);
        animator.SetTrigger("Die");
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
