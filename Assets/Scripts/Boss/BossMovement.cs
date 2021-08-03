using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class BossMovement : MonoBehaviour
{
    [SerializeField] private float visionRadius;
    [SerializeField] private float attackDistance;
    [SerializeField] private Transform playerPos;
    [SerializeField] private AudioClip audioClip;

    private Animator animator;
    private HumanAudioSourceController humanAudioSourceController;
    private BossController bossController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        humanAudioSourceController = GetComponent<HumanAudioSourceController>();

        bossController = GetComponent<BossController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossController.IsAlive)
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
                        animator.SetBool("Attack", true);
                        animator.SetBool("Move", false);
                        //audioSource.Stop();
                    }
                }
                else
                {
                    animator.SetBool("Move", true);
                    animator.SetBool("Attack", false);
                    humanAudioSourceController.PlayStepAudio();
                    transform.position = Vector3.MoveTowards(transform.position, playerPos.position, 4f * Time.deltaTime);
                }

                transform.rotation = newRotation;
                return;
            }
        }

        //// если игрок выходит из поля зрения, продолжаем патрулировать
        //else animator.SetBool("Move", false);
    }
}
