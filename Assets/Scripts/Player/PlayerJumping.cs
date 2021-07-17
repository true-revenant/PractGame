using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    // JUMP
    public float jumpHeightK;
    private bool isGround = true;
    private Animator animator;
    private PlayerController playerController;
    private AudioSource audioSource;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /// JUMPING ///
        if (playerController.IsAlive)
        {
            // проверка положения перед прыжком, бросаем луч вниз длиной в высоту персонажа
            RaycastHit raycastHit;
            var raycast = Physics.Raycast(transform.GetChild(0).GetChild(0).position, Vector3.down, out raycastHit, 1.5f);
            isGround = raycast ? true : false;

            //animator.SetBool("Jump", !isGround);

            // прыжок
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (isGround)
                {
                    animator.SetBool("Jump", isGround);
                }
                else
                {
                    audioSource.Stop();
                }
            }
            //else animator.SetBool("Jump", false);
        }
    }

    public void Jump()
    {
        Debug.Log("ПРЫЖОК!!");
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeightK, ForceMode.Impulse);
        animator.SetBool("Jump", false);
    }
}
