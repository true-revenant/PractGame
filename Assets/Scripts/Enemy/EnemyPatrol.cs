using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

internal sealed class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    private NavMeshAgent navMeshAgent;
    private int currentWaypointIndex;
    private Animator animator;
    
    private EnemyController enemyController;
    private EnemyBehave enemyBehave;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(_waypoints[0].position);
        
        animator = GetComponent<Animator>();
        
        enemyController = GetComponent<EnemyController>();
        enemyBehave = GetComponent<EnemyBehave>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        animator.SetBool("Move", true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (enemyController.IsAlive)
        {
            // если игрок зашел в поле зрения врага, выключение режима патруля и переключение на игрока
            if (!enemyBehave.EnemyDetectPlayer(0.5f))
            {
                animator.SetBool("Move", false);
                Invoke("backToPatrol", 1f);
            }
            else navMeshAgent.enabled = false;

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
}
