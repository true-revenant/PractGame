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

    private Vector3 _direction;
    private bool isGround = true;

    private void Awake()
    {
        IsAlive = true;
        maxHP = 100;
        currentHP = maxHP;
        Debug.Log("Awake()");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");

        transform.Translate(_direction * _movingSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));

        // Бросок гранаты
        if (Input.GetMouseButtonDown(1)) CreateBomb();

        // выстрел
        if (Input.GetMouseButton(0)) CreateBullet();

        // проверка положения перед прыжком, бросаем луч вниз длинной в высоту персонажа
        RaycastHit raycastHit;
        var raycast = Physics.Raycast(transform.GetChild(0).position, Vector3.down, out raycastHit, 1.5f);

        isGround = raycast ? true : false;

        // прыжок
        if (Input.GetKeyDown(KeyCode.Q) && isGround)
        {
            Debug.Log("ПРЫЖОК!!");
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeightK, ForceMode.Impulse);
        }
    }

    private void CreateBomb()
    {
        //Debug.Log("Create Bomb!");
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
        
        
        
        
        RaycastHit hit;
        var raycast = Physics.Raycast(_bulletStartPos.position, transform.forward, out hit, Mathf.Infinity);
        
        if (raycast)
        {
            Debug.Log($"Попали в {hit.collider.gameObject.tag}");
            if (hit.collider.gameObject.CompareTag("Enemy") || hit.collider.gameObject.CompareTag("EnemyPatrol"))
                hit.collider.gameObject.GetComponent<LiveObj>().TakeDamage(50);
        }
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log($"{name} : - {damage} HP!");
        currentHP -= damage;

        if (currentHP <= 0) Die();
    }

    public void TakeHeal()
    {
        currentHP += 15;
        if (currentHP >= maxHP) currentHP = maxHP;
        Debug.Log($"{name} : HEALED!!! +15HP");
    }

    private void Die()
    {
        Debug.Log($"{name} : DEAD!!!");
    }
}
