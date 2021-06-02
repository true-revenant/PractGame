using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject _bombPref;
    public GameObject _bulletPref;
    public Transform _bombStartPos;
    public Transform _bulletStartPos;

    public float _rotationSpeed = 3f;
    public float _movingSpeed = 3f;
    public float _explosion_timer = 1f;

    private Vector3 _direction;
    private int health = 100;

    private void Awake()
    {
        Debug.Log("Awake()");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"ROTATION = {transform.rotation}");
        //Debug.Log($"LOCAL ROTATION = {transform.localRotation}");

        //Debug.Log("Update()");

        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");

        //_direction.z = -_direction.z;
        //_direction.x = -_direction.x;

        //Debug.Log($"FIRE 1 = {Input.GetAxis("Fire1")}");

        transform.Translate(_direction * _movingSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));

        // 2 способа обработки кнопки огня
        if (Input.GetMouseButtonDown(1)) CreateBomb();
        //if (Input.GetAxis("Fire1") > 0) CreateBomb();

        if (Input.GetMouseButtonDown(0)) CreateBullet();

    }

    private void LateUpdate()
    {
        //Debug.Log("LateUpdate()");
    }

    private void FixedUpdate() {}

    private void CreateBomb()
    {
        //Debug.Log("Create Bomb!");
        var bomb = Instantiate(_bombPref, _bombStartPos.position, transform.rotation).GetComponent<Bomb>();
        bomb.Init(_explosion_timer);

    }

    private void CreateBullet()
    {
        //Debug.Log("Create Bullet!");
        //Instantiate(_bulletPref, _bulletStartPos.position, transform.rotation)
        var rBody = Instantiate(_bulletPref, _bulletStartPos.position, Quaternion.identity).GetComponent<Rigidbody>();
        rBody.velocity = _bulletStartPos.forward * 15f;
        //bullet.Init(_enemyPos);
    }

    public void TakeDamage()
    {
        Debug.Log($"{name} : OUCH!!! -5HP");
        health -= 5;

        if (health <= 0) Die();
    }

    public void TakeHeal()
    {
        health += 15;
        Debug.Log($"{name} : HEALED!!! +15HP");
    }

    private void Die()
    {
        Debug.Log($"{name} : DEAD!!!");
    }
}
