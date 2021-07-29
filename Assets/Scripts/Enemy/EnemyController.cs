using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ILiveObj, ITakeDamage, ITakeExplosionDamage
{
    private Animator animator;
    private ParticleSystem _particleSystem;

    public int maxHP { get; set; }
    public int currentHP { get; set; }
    public bool IsAlive { get; set; }

    private void Awake()
    {
        maxHP = 50;
        currentHP = maxHP;
        IsAlive = true;

        animator = GetComponent<Animator>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void TakeDamage(int damage)
    {
        _particleSystem.Play();
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
