using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Turrel : LiveObj
{
    private float _rotationDirectionSign = 1;

    [SerializeField] private Transform _playerPos;
    [SerializeField] private float _minDistance = 3f;
    [SerializeField] private float _rotationSpeed = 3f;

    public GameObject _bulletPref;
    public Transform _bulletStartPos;

    private void Awake()
    {
        maxHP = 50;
        currentHP = maxHP;
        IsAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _playerPos.position) < _minDistance)
        {
            Vector3 relative = _playerPos.position - transform.position;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, relative, _rotationSpeed * Time.deltaTime, 0f);
            var newRotation = Quaternion.LookRotation(newDir);

            // ���� ������� ������� �� ������, �� ��������
            if (Quaternion.Angle(transform.rotation, newRotation) == 0)
            {
                CreateBullet();
            }

            transform.rotation = newRotation;

            //if (!shotMade)
            //{
            //    CreateBullet();
            //    shotMade = true;
            //}

        }
        else InititalRotation();
    }

    private void InititalRotation()
    {
        // changing rotation direction
        if (transform.rotation.y >= 0.9 && _rotationDirectionSign > 0) _rotationDirectionSign = -_rotationDirectionSign;
        else if (transform.rotation.y <= -0.9 && _rotationDirectionSign < 0) _rotationDirectionSign = -_rotationDirectionSign;

        transform.Rotate(Vector3.up * Time.deltaTime * 25 * _rotationDirectionSign);
    }

    private void CreateBullet()
    {
        //Instantiate(_bulletPref, _bulletStartPos.position, transform.rotation);
        var rBody = Instantiate(_bulletPref, _bulletStartPos.position, Quaternion.identity).GetComponent<Rigidbody>();
        rBody.velocity = _bulletStartPos.forward * 15f;
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log($"{name} : Took Damage!!!");
        currentHP -= damage;
        if (currentHP <= 0 && IsAlive)
        {
            //StartCoroutine(DeathAnimation());
        }
    }

    //IEnumerator DeathAnimation()
    //{

    //}
}
