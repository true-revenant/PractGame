using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Turrel : MonoBehaviour
{
    Timer timer;
    //private bool shotMade = false;

    [SerializeField] private Transform _playerPos;
    [SerializeField] private float _minDistance = 3f;
    [SerializeField] private float _rotationSpeed = 3f;

    public GameObject _bulletPref;
    public Transform _bulletStartPos;

    private float _rotationDirectionSign = 1;

    private void Awake()
    {
        timer = new Timer(1000);
        timer.Elapsed += Timer_Elapsed;
        
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e) {}

    // Start is called before the first frame update
    void Start() 
    {
        timer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _playerPos.position) < _minDistance)
        {
            Vector3 relative = _playerPos.position - transform.position;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, relative, _rotationSpeed * Time.deltaTime, 0f);
            var newRotation = Quaternion.LookRotation(newDir);

            //Debug.Log($"CURR ANGLE = {Quaternion.Angle(transform.rotation, newRotation)}");
            if (Quaternion.Angle(transform.rotation, newRotation) == 0)
            {
                Debug.Log($"TURREL LOOK AT PLAYER!");

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
        Debug.Log("Create Bullet!");
        //Instantiate(_bulletPref, _bulletStartPos.position, transform.rotation);
        var rBody = Instantiate(_bulletPref, _bulletStartPos.position, Quaternion.identity).GetComponent<Rigidbody>();
        rBody.velocity = _bulletStartPos.forward * 15f;
    }
}
