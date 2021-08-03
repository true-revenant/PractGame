using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class PlayerJumping : MonoBehaviour
{
    // JUMP
    public float jumpHeightK;
    private Animator animator;
    private PlayerController playerController;

    //SOUND
    private HumanAudioSourceController humanAudioSourceController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        humanAudioSourceController = GetComponent<HumanAudioSourceController>();
    }

    // Update is called once per frame
    void Update()
    {
        /// JUMPING ///
        if (playerController.IsAlive)
        {
            // ïðûæîê
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (isGround())
                {
                    animator.SetBool("Jump", isGround());
                    humanAudioSourceController.StopAudio();
                }
                else { }
            }
        }
    }

    public bool isGround()
    {
        RaycastHit raycastHit;
        var raycast = Physics.Raycast(transform.GetChild(0).GetChild(0).position, Vector3.down, out raycastHit, 1.5f);
        return raycast ? true : false;
    }

    public void Jump()
    {
        Debug.Log("ÏÐÛÆÎÊ!!");
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeightK, ForceMode.Impulse);
        animator.SetBool("Jump", false);
    }
}
