using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;
    private int currentWaypointIndex;

    private void Awake()
    {
        //gameObject.SetActive(false);
        //Debug.Log($"Enemy Tag = {gameObject.tag}");

        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(_waypoints[0].position);
    }

    // Start is called before the first frame update
    private void Start()
    {
        var playerGO = GameObject.Find("Player");
        if (playerGO != null)
        {
            Debug.Log("Player is found!");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // реализация цикличности к обращению к каждому элементу массива точек перемещения
        // (изящно)
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % _waypoints.Length;
            navMeshAgent.SetDestination(_waypoints[currentWaypointIndex].position);
        }
    }

    public void TakeDamage()
    {
        Debug.Log($"{name} : OUCH!!!");
    }
}
