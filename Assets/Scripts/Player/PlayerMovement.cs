using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // MOVE
    public float _rotationSpeed = 3f;
    public float _movingSpeed = 3f;
    private Vector3 _direction;
    private Animator animator;
    private PlayerController playerController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        /// MOVEMENT ///
        if (playerController.IsAlive)
        {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");

            if (_direction != Vector3.zero) animator.SetBool("Move", true);
            else animator.SetBool("Move", false);

            transform.Translate(_direction * _movingSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));
        }
    }
}