using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float visionRadius;
    [SerializeField] private float attackDistance;
    [SerializeField] private Transform playerPos;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ����� ����� � ���� ������ �����, �������� �������� � ������� ������
        if (Vector3.Distance(transform.position, playerPos.position) <= visionRadius && transform.position.y < 0.5)
        {
            Vector3 relative = playerPos.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, relative, 5f * Time.deltaTime, 0f);
            Quaternion newRotation = Quaternion.LookRotation(newDir);

            animator.SetBool("Move", true);

            // ���� ����� ����� � ���� ������������ �����, �� ��������� ������
            if (Vector3.Distance(transform.position, playerPos.position) <= attackDistance)
            {
                // ���� ���� ������� �� ������, �� ��������
                if (Quaternion.Angle(transform.rotation, newRotation) == 0)
                {
                    animator.SetBool("Attack", true);
                    animator.SetBool("Move", false);
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPos.position, 4f * Time.deltaTime);
            }

            transform.rotation = newRotation;
            return;
        }
        //// ���� ����� ������� �� ���� ������, ���������� �������������
        //else animator.SetBool("Move", false);
    }
}
