using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Door _door;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter() Called!");
        if (other.CompareTag("Player"))
        {
            _door.doorIsOpening = true;
        }
    }

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {}
}
