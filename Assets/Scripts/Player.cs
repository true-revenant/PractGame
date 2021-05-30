using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject _bombPref;
    public Transform _bombStartPos;
    public float _speed = 3f;
    public float _explosion_timer = 1f;

    private Vector3 _direction;

    private void Awake()
    {
        Debug.Log("Awake()");
    }

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update()");

        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");

        //_direction.z = -_direction.z;
        //_direction.x = -_direction.x;

        Debug.Log($"FIRE 1 = {Input.GetAxis("Fire1")}");

        var speed = _direction * _speed * Time.deltaTime;
        transform.Translate(speed);


        // 2 способа обработки кнопки огня

        if (Input.GetMouseButtonDown(0)) CreateBomb();
        //if (Input.GetAxis("Fire1") > 0) CreateBomb();

    }

    private void LateUpdate()
    {
        Debug.Log("LateUpdate()");
    }

    private void FixedUpdate() {}

    private void CreateBomb()
    {
        Debug.Log("Create Bomb!");
        var bomb = Instantiate(_bombPref, _bombStartPos.position, Quaternion.identity).GetComponent<Bomb>();
        bomb.Init(_explosion_timer);

    }

    public void TakeDamage()
    {
        Debug.Log($"{name} : OUCH!!!");
    }
}
