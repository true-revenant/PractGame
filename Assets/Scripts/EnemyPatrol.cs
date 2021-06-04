using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : LiveObj
{
    [SerializeField] private Transform[] _waypoints;

    private Color deathColor;
    private MeshRenderer meshRenderer;
    private NavMeshAgent navMeshAgent;
    private int currentWaypointIndex;

    [SerializeField] private float visionRadius;
    [SerializeField] private float attackDistance;
    [SerializeField] private Transform playerPos;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform bulletStartPos;

    private void Awake()
    {
        deathColor = Color.white;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(_waypoints[0].position);

        // �������� ��� ������
        meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();

        maxHP = 50;
        currentHP = maxHP;
        IsAlive = true;
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
        if (IsAlive)
        {
            // ���� ����� ����� � ���� ������ �����, ���������� ������ ������� � ������������ �� ������
            if (Vector3.Distance(transform.position, playerPos.position) <= visionRadius && transform.position.y < 0.5)
            {
                navMeshAgent.enabled = false;
                Vector3 relative = playerPos.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, relative, 5f * Time.deltaTime, 0f);
                Quaternion newRotation = Quaternion.LookRotation(newDir);

                // ���� ����� ����� � ���� ������������ �����, �� ��������� ������
                if (Vector3.Distance(transform.position, playerPos.position) <= attackDistance)
                {
                    // ���� ���� ������� �� ������, �� ��������
                    if (Quaternion.Angle(transform.rotation, newRotation) == 0)
                        CreateBullet();
                }
                else transform.position = Vector3.MoveTowards(transform.position, playerPos.position, 2f * Time.deltaTime);

                transform.rotation = newRotation;
                return;
            }
            // ���� ����� ������� �� ���� ������, ���������� �������������
            else Invoke("backToPatrol", 1f);

            // ���������� ����������� � ��������� � ������� �������� ������� ����� �����������
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

    IEnumerator DeathAnimation()
    {
        IsAlive = false;
        // �������� ��������� �� ����
        while (meshRenderer.material.color != deathColor)
        {
            // ������� ������� �� ������ ����� � ������
            meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, deathColor, 0.1f);
            // �������� ���� ���������� ������� Update()
            yield return null;
        }
        navMeshAgent.enabled = false;
        gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        
        // ���� ���������, �� �� ������� ������ � ����, ������� 3 ������� � ���������� ������
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
