using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Turrel : MonoBehaviour, ILiveObj, ITakeExplosionDamage
{
    private float _rotationDirectionSign = 1;
    private BulletAttack bulletAttack;
    private TurretAudioSourceController turretAudioSourceController;
    private PlayerController playerController;

    [SerializeField] private Transform _playerPos;
    [SerializeField] private float _minDistance = 3f;
    [SerializeField] private float _rotationSpeed = 3f;

    public int maxHP { get; set; }
    public int currentHP { get; set; }
    public bool IsAlive { get; set; }

    private void Awake()
    {
        maxHP = 100;
        currentHP = maxHP;
        IsAlive = true;
        bulletAttack = GetComponent<BulletAttack>();
        turretAudioSourceController = GetComponent<TurretAudioSourceController>();

        playerController = _playerPos.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive)
        {
            if (Vector3.Distance(transform.position, _playerPos.position) < _minDistance && playerController.IsAlive)
            {
                Vector3 relative = _playerPos.position - transform.position;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, relative, _rotationSpeed * Time.deltaTime, 0f);
                var newRotation = Quaternion.LookRotation(newDir);

                // Если туррель смотрит на игрока, то стреляет
                if (Quaternion.Angle(transform.rotation, newRotation) == 0)
                {
                    bulletAttack.CreateBullet();
                    turretAudioSourceController.StartAudio();
                }
                else turretAudioSourceController.StopAudio();

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
