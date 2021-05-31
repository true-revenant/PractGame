using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrel : MonoBehaviour
{
    [SerializeField] private Transform _playerPos;
    [SerializeField] private float _minDistance = 3f;
    [SerializeField] private float _rotationSpeed = 3f;
    
    private float _rotationDirectionSign = 1;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _playerPos.position) < _minDistance)
        {
            Vector3 relative = _playerPos.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, relative, _rotationSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);
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
}
