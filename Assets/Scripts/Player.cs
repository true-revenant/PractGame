using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LiveObj
{
    public GameObject _bombPref;
    public GameObject _bulletPref;
    public Transform _bombStartPos;
    public Transform _bulletStartPos;
    public float _rotationSpeed = 3f;
    public float _movingSpeed = 3f;
    public float _force;
    public float jumpHeightK;

    [SerializeField] private HealthLine healthLine;
    private Vector3 _direction;
    private bool isGround = true;
    private Animator animator;
    private bool weaponIsReloaded = true;
    private bool bombIsReloaded = true;
    private float reloadTime = 0.5f;

    public bool blueKeyCollected { get; set; } = true;
    public bool orangeKeyCollected { get; set; } = true;

    private void Awake()
    {
        IsAlive = true;
        maxHP = 100;
        currentHP = maxHP;

        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");

        if (_direction != Vector3.zero) animator.SetBool("Move", true);
        else animator.SetBool("Move", false);

        transform.Translate(_direction * _movingSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));

        // Бросок гранаты
        if (Input.GetMouseButtonDown(1) && bombIsReloaded)
        {
            animator.SetBool("ThrowBomb", true);
            bombIsReloaded = false;
            Invoke("BombReload", reloadTime);
        }
        
        // выстрел
        if (Input.GetMouseButton(0) && weaponIsReloaded)
        {
            animator.SetBool("Shoot", true);
            weaponIsReloaded = false;
            Invoke("WeaponReload", reloadTime);
        }
        
        // проверка положения перед прыжком, бросаем луч вниз длиной в высоту персонажа
        RaycastHit raycastHit;
        var raycast = Physics.Raycast(transform.GetChild(0).position, Vector3.down, out raycastHit, 1.5f);
        isGround = raycast ? true : false;
        
        //animator.SetBool("Jump", !isGround);

        // прыжок
        if (Input.GetKeyDown(KeyCode.Q) && isGround)
        {
            animator.SetBool("Jump", isGround);
        }
        //else animator.SetBool("Jump", false);

    }

    private void CreateBomb()
    {
        var rBody = Instantiate(_bombPref, _bombStartPos.position, transform.rotation).GetComponent<Rigidbody>();
        rBody.AddForce(transform.forward * _force, ForceMode.Impulse);
        
        //bomb.GetComponent<Rigidbody>().AddTorque(10f * Vector3.forward);
    }

    private void CreateBullet()
    {
        //Debug.Log("Create Bullet!");
        //Instantiate(_bulletPref, _bulletStartPos.position, transform.rotation)
        
        //var rBody = Instantiate(_bulletPref, _bulletStartPos.position, Quaternion.identity).GetComponent<Rigidbody>();
        //rBody.velocity = _bulletStartPos.forward * 25f;
        
        ////////////////

        RaycastHit hit;
        var raycast = Physics.Raycast(_bulletStartPos.position, transform.forward, out hit, Mathf.Infinity);
        
        if (raycast)
        {
            Debug.Log($"Попали в {hit.collider.gameObject.tag}");
            if (hit.collider.gameObject.CompareTag("Enemy") || hit.collider.gameObject.CompareTag("EnemyPatrol"))
                hit.collider.gameObject.GetComponent<LiveObj>().TakeDamage(50);
        }
    }

    private void Jump()
    {
        Debug.Log("ПРЫЖОК!!");
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeightK, ForceMode.Impulse);
        animator.SetBool("Jump", false);
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log($"{name} : - {damage} HP!");
        currentHP -= damage;
        healthLine.DecreaseHealthlineValue(damage);

        if (currentHP <= 0) Die();
    }

    public void TakeHeal()
    {
        currentHP += 15;
        healthLine.IncreaseHealthlineValue(15);
        if (currentHP >= maxHP) currentHP = maxHP;
        Debug.Log($"{name} : HEALED!!! +15HP");
    }

    private void Die()
    {
        Debug.Log($"{name} : DEAD!!!");
    }

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

    public override void DeadByExplosion()
    {
        throw new System.NotImplementedException();
    }
}
