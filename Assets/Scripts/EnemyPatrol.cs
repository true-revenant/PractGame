using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : LiveObj
{
    [SerializeField] private Transform[] _waypoints;

    private NavMeshAgent navMeshAgent;
    private int currentWaypointIndex;
    private Animator animator;

    [SerializeField] private float visionRadius;
    [SerializeField] private float attackDistance;
    [SerializeField] private Transform playerPos;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform bulletStartPos;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(_waypoints[0].position);
        animator = GetComponent<Animator>();

        maxHP = 50;
        currentHP = maxHP;
        IsAlive = true;
    }

    // Start is called before the first frame update
    private void Start()
    {
        animator.SetBool("Move", true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsAlive)
        {
            // если игрок зашел в поле зрения врага, выключение режима патруля и переключение на игрока
            if (Vector3.Distance(transform.position, playerPos.position) <= visionRadius && transform.position.y < 0.5)
            {
                navMeshAgent.enabled = false;
                Vector3 relative = playerPos.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, relative, 5f * Time.deltaTime, 0f);
                Quaternion newRotation = Quaternion.LookRotation(newDir);

                // если игрок зашел в поле досягаемости атаки, то атаковать игрока
                if (Vector3.Distance(transform.position, playerPos.position) <= attackDistance)
                {
                    // Если враг смотрит на игрока, то стреляет
                    if (Quaternion.Angle(transform.rotation, newRotation) == 0)
                    {
                        //CreateBullet();
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
            else
            {
                animator.SetBool("Move", false);
                Invoke("backToPatrol", 1f);
            }
            
            // реализация цикличности к обращению к каждому элементу массива точек перемещения
            if (navMeshAgent.enabled && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % _waypoints.Length;
                navMeshAgent.SetDestination(_waypoints[currentWaypointIndex].position);
            }
        }
    }

    private void backToPatrol()
    {
        navMeshAgent.enabled = true;
        animator.SetBool("Move", true);
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log($"{name} : - {damage} HP!!!");
        currentHP -= damage;
        if (currentHP <= 0 && IsAlive)
        {
            StartCoroutine(DeathAnimation());
        }
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
        animator.SetTrigger("Die");
        IsAlive = false;
        //animator.SetBool("Die", true);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public override void DeadByExplosion()
    {
        StartCoroutine(DeathByExplosionAnimation());
    }
}
