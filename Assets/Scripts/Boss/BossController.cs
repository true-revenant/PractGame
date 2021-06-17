using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : LiveObj, ITakeDamage
{
    [SerializeField] private HealthLine healthLine;

    private Animator animator;
    private NotificationManager notificationManager;

    private void Awake()
    {
        maxHP = 100;
        currentHP = maxHP;
        IsAlive = true;
        animator = GetComponent<Animator>();
        notificationManager = GetComponent<NotificationManager>();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{name} : OUCH!!!");
        currentHP -= damage;
        Debug.Log($"BOSS currentHP = {currentHP}");
        healthLine.DecreaseHealthlineValue(damage);
        if (currentHP <= 0 && IsAlive)
        {
            Debug.Log("CURRENT HP <= 0");
            StartCoroutine(DeathAnimation());
        }
    }

    IEnumerator DeathAnimation()
    {
        IsAlive = false;
        animator.SetTrigger("Die");
        Debug.Log("BOSS IS DEAD!");
        //gameObject.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        notificationManager.ShowNotification("онаедю!");
    }
}
