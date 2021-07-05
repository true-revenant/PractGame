using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ILiveObj, ITakeDamage
{
    [SerializeField] private HealthLine healthLine;
    private Animator animator;
    private NotificationManager notificationManager;

    public bool blueKeyCollected { get; set; } = false;
    public bool orangeKeyCollected { get; set; } = false;
    public int maxHP { get; set; }
    public int currentHP { get; set; }
    public bool IsAlive { get; set; }

    private void Awake()
    {
        IsAlive = true;
        maxHP = 100;
        currentHP = maxHP;

        animator = GetComponent<Animator>();
        notificationManager = GetComponent<NotificationManager>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{name} : - {damage} HP!");
        currentHP -= damage;
        healthLine.DecreaseHealthlineValue(damage);

        if (currentHP <= 0 && IsAlive) StartCoroutine(DeathAnimation());
    }

    public void TakeHeal()
    {
        currentHP += 15;
        healthLine.IncreaseHealthlineValue(15);
        if (currentHP >= maxHP) currentHP = maxHP;
        Debug.Log($"{name} : HEALED!!! +15HP");
    }

    IEnumerator DeathAnimation()
    {
        animator.SetTrigger("Death");
        IsAlive = false;
        //gameObject.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(2);
        notificationManager.ShowNotification("бШ сахрш!");
    }
}
