using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LiveObj
{
    //private Color deathColor;
    //private MeshRenderer meshRenderer;
    private Animator animator;
    private Transform playerPos;

    [SerializeField] private float visionRadius;
    [SerializeField] private float attackDistance; 
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform bulletStartPos;

    private void Awake()
    {
        maxHP = 50;
        currentHP = maxHP;
        IsAlive = true;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsAlive)
        {
            // если игрок зашел в поле зрения врага, начинаем движение в сторону игрока
            if (Vector3.Distance(transform.position, playerPos.position) <= visionRadius && transform.position.y < 0.5)
            {
                Vector3 relative = playerPos.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, relative, 5f * Time.deltaTime, 0f);
                Quaternion newRotation = Quaternion.LookRotation(newDir);

                // если игрок зашел в поле досягаемости атаки, то атаковать игрока
                if (Vector3.Distance(transform.position, playerPos.position) <= attackDistance)
                {
                    // Если враг смотрит на игрока, то стреляет
                    if (Quaternion.Angle(transform.rotation, newRotation) == 0)
                    {
                        animator.SetBool("Move", false);
                        animator.SetBool("Shoot", true);
                    }
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, playerPos.position, 2f * Time.deltaTime);
                    animator.SetBool("Move", true);
                    animator.SetBool("Shoot", false);
                }

                transform.rotation = newRotation;
                return;
            }
            // если игрок выходит из поля зрения, продолжаем патрулировать
            else animator.SetBool("Move", false);
        }
    }

    public void Init(Transform playerPos)
    {
        this.playerPos = playerPos;
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log($"{name} : OUCH!!!");
        currentHP -= damage;
        if (currentHP <= 0 && IsAlive)
        {
            StartCoroutine(DeathAnimation());
        }
    }

    public override void DeadByExplosion()
    {
        StartCoroutine(DeathByExplosionAnimation());
    }

    private void CreateBullet()
    {
        //Instantiate(_bulletPref, _bulletStartPos.position, transform.rotation);
        var rBody = Instantiate(bulletPref, bulletStartPos.position, Quaternion.identity).GetComponent<Rigidbody>();
        rBody.velocity = bulletStartPos.forward * 15f;
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
