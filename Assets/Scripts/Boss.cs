using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : LiveObj
{
    [SerializeField] private float visionRadius;
    [SerializeField] private float attackDistance;
    [SerializeField] private Transform playerPos;
    [SerializeField] private GameObject bombPref;
    [SerializeField] private Transform bombStartPos;
    [SerializeField] private float force;

    private Animator animator;

    private void Awake()
    {
        maxHP = 200;
        currentHP = maxHP;
        IsAlive = true;
        animator = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive)
        {
            // если игрок зашел в поле зрения врага, начинаем движение в сторону игрока
            if (Vector3.Distance(transform.position, playerPos.position) <= visionRadius && transform.position.y < 0.5)
            {
                Vector3 relative = playerPos.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, relative, 5f * Time.deltaTime, 0f);
                Quaternion newRotation = Quaternion.LookRotation(newDir);

                animator.SetBool("Move", true);

                // если игрок зашел в поле досягаемости атаки, то атаковать игрока
                if (Vector3.Distance(transform.position, playerPos.position) <= attackDistance)
                {
                    // Если враг смотрит на игрока, то стреляет
                    if (Quaternion.Angle(transform.rotation, newRotation) == 0)
                    {
                        animator.SetBool("Attack", true);
                        animator.SetBool("Move", false);
                    }
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, playerPos.position, 2f * Time.deltaTime);
                }

                transform.rotation = newRotation;
                return;
            }
            // если игрок выходит из поля зрения, продолжаем патрулировать
            else
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Move", false);
            }
        }
    }
    public override void DeadByExplosion() { }

    public override void TakeDamage(int damage) 
    {
        Debug.Log($"{name} : OUCH!!!");
        currentHP -= damage;
        if (currentHP <= 0 && IsAlive)
        {
            StartCoroutine(DeathAnimation());
        }
    }

    private void CreateBomb()
    {
        var rBody = Instantiate(bombPref, bombStartPos.position, transform.rotation).GetComponent<Rigidbody>();
        rBody.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    IEnumerator DeathAnimation()
    {
        IsAlive = false;
        animator.SetTrigger("Die");
        //gameObject.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
