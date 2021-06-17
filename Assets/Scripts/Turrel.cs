using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Turrel : LiveObj, ITakeExplosionDamage, IAttack
{
    private float _rotationDirectionSign = 1;

    [SerializeField] private Transform _playerPos;
    [SerializeField] private float _minDistance = 3f;
    [SerializeField] private float _rotationSpeed = 3f;

    public GameObject _bulletPref;
    public Transform _bulletStartPos;

    private void Awake()
    {
        maxHP = 100;
        currentHP = maxHP;
        IsAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive)
        {
            if (Vector3.Distance(transform.position, _playerPos.position) < _minDistance)
            {
                Vector3 relative = _playerPos.position - transform.position;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, relative, _rotationSpeed * Time.deltaTime, 0f);
                var newRotation = Quaternion.LookRotation(newDir);

                // Если туррель смотрит на игрока, то стреляет
                if (Quaternion.Angle(transform.rotation, newRotation) == 0)
                {
                    CreateBullet();
                }

                transform.rotation = newRotation;
            }
            else InititalRotation();
        }
    }

    private void InititalRotation()
    {
        // changing rotation direction
        if (transform.rotation.y >= 0.9 && _rotationDirectionSign > 0) _rotationDirectionSign = -_rotationDirectionSign;
        else if (transform.rotation.y <= -0.9 && _rotationDirectionSign < 0) _rotationDirectionSign = -_rotationDirectionSign;

        transform.Rotate(Vector3.up * Time.deltaTime * 25 * _rotationDirectionSign);
    }

    public void CreateBullet()
    {
        //Instantiate(_bulletPref, _bulletStartPos.position, transform.rotation);
        var rBody = Instantiate(_bulletPref, _bulletStartPos.position, Quaternion.identity).GetComponent<Rigidbody>();
        rBody.velocity = _bulletStartPos.forward * 15f;
    }

    public void DeadByExplosion()
    {
        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        IsAlive = false;
        GetComponent<Rigidbody>().freezeRotation = false;
        GetComponent<Rigidbody>().AddExplosionForce(600f, transform.position, 5f, 600f, ForceMode.Impulse);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
