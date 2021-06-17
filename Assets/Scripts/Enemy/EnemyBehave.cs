using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehave : MonoBehaviour
{
    private Animator animator;
    private EnemyController enemyController;

    [SerializeField] Transform playerPos;
    [SerializeField] private float visionRadius;
    [SerializeField] private float attackDistance;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyController.IsAlive)
        {
            // ���� ����� ����� � ���� ������ �����, �������� �������� � ������� ������
            if (EnemyDetectPlayer(0.5f))
            {
                Vector3 relative = playerPos.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, relative, 5f * Time.deltaTime, 0f);
                Quaternion newRotation = Quaternion.LookRotation(newDir);

                // ���� ����� ����� � ���� ������������ �����, �� ��������� ������
                if (Vector3.Distance(transform.position, playerPos.position) <= attackDistance)
                {
                    // ���� ���� ������� �� ������, �� ��������
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
            //// ���� ����� ������� �� ���� ������, ���������� �������������
            //else animator.SetBool("Move", false);
        }
    }

    public void Init(Transform playerPos)
    {
        this.playerPos = playerPos;
    }

    public bool EnemyDetectPlayer(float visionDistance)
    {
        return Vector3.Distance(transform.position, playerPos.position) <= visionRadius && transform.position.y < visionDistance;
    }
}
