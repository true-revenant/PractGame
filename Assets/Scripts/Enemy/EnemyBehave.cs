using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehave : MonoBehaviour
{
    private Animator animator;
    private EnemyController enemyController;
    private HumanAudioSourceController humanAudioSourceController;
    private PlayerController playerController;

    [SerializeField] Transform playerPos;
    [SerializeField] private float visionRadius;
    [SerializeField] private float attackDistance;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
        humanAudioSourceController = GetComponent<HumanAudioSourceController>();
    }

    private void Start()
    {
        playerController = playerPos.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyController.IsAlive)
        {
            // если игрок зашел в поле зрения врага, начинаем движение в сторону игрока
            if (EnemyDetectPlayer(0.5f) && playerController.IsAlive)
            {
                Vector3 relative = playerPos.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, relative, 5f * Time.deltaTime, 0f);
                Quaternion newRotation = Quaternion.LookRotation(newDir);

                // если игрок зашел в поле досягаемости атаки, то атаковать игрока
                if (Vector3.Distance(transform.position, playerPos.position) <= attackDistance && playerController.IsAlive)
                {
                    // Если враг смотрит на игрока, то стреляет
                    if (Quaternion.Angle(transform.rotation, newRotation) == 0 && playerController.IsAlive)
                    {
                        animator.SetBool("Move", false);
                        animator.SetBool("Shoot", true);
                        humanAudioSourceController.StopAudio();
                    }
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, playerPos.position, 2f * Time.deltaTime);
                    animator.SetBool("Move", true);
                    animator.SetBool("Shoot", false);
                    humanAudioSourceController.PlayStepAudio();
                }

                transform.rotation = newRotation;
                return;
            }
            //// если игрок выходит из поля зрения, продолжаем патрулировать
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
