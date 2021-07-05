using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : BombAttack
{
    // ATTACK
    private PlayerController playerController;
    //private GameObject _bombPref;   
    private bool weaponIsReloaded = true;
    private bool bombIsReloaded = true;
    private float reloadTime = 0.5f;
    private Animator animator;

    //public float _force;
    [SerializeField] private Transform _attackStartPos;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        //_bombPref = (GameObject)Resources.Load("Prefabs/Bomb");
    }

    // Update is called once per frame
    void Update()
    {
        /// ATTACK ///

        if (playerController.IsAlive)
        {
            // ������ �������
            if (Input.GetMouseButtonDown(1) && bombIsReloaded)
            {
                animator.SetBool("ThrowBomb", true);
                bombIsReloaded = false;
                Invoke("BombReload", reloadTime);
            }

            // �������
            if (Input.GetMouseButton(0) && weaponIsReloaded)
            {
                animator.SetBool("Shoot", true);
                weaponIsReloaded = false;
                Invoke("WeaponReload", reloadTime);
            }
        }
    }

    //public void CreateBomb()
    //{
    //    var rBody = Instantiate(_bombPref, _attackStartPos.position, transform.rotation).GetComponent<Rigidbody>();
    //    rBody.AddForce(transform.forward * _force, ForceMode.Impulse);

    //    //bomb.GetComponent<Rigidbody>().AddTorque(10f * Vector3.forward);
    //}

    public void CreateRaycastBullet()
    {
        RaycastHit hit;
        var raycast = Physics.Raycast(_attackStartPos.position, transform.forward, out hit, Mathf.Infinity);

        if (raycast)
        {
            Debug.Log($"������ � {hit.collider.gameObject.tag}");
            if (hit.collider.gameObject.CompareTag("Enemy") || hit.collider.gameObject.CompareTag("EnemyPatrol"))
                hit.collider.gameObject.GetComponent<ITakeDamage>().TakeDamage(50);
            else if (hit.collider.gameObject.CompareTag("Boss"))
                hit.collider.gameObject.GetComponent<ITakeDamage>().TakeDamage(1);

        }
    }

    //public void CreateBullet()
    //{
    //    Debug.Log("Create Bullet!");
    //    Instantiate(_bulletPref, _bulletStartPos.position, transform.rotation)

    //    var rBody = Instantiate(_bulletPref, _bulletStartPos.position, Quaternion.identity).GetComponent<Rigidbody>();
    //    rBody.velocity = _bulletStartPos.forward * 25f;
    //}


    private void WeaponReload()
    {
        weaponIsReloaded = true;
        animator.SetBool("Shoot", false);
    }

    private void BombReload()
    {
        bombIsReloaded = true;
        animator.SetBool("ThrowBomb", false);
    }
}
