using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyKeeper : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
    private Vector3 keyPosition;
    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        keyPosition = transform.position;
        anim = GetComponent<Animation>();
    }

    private void DestroyTriggerShowKey()
    {
        Destroy(gameObject);
        Instantiate(keyPrefab, keyPosition, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (anim.isPlaying)
            {
                Debug.Log("ANIMATION IS PLAYING!!");
                
                if (gameObject.name == "LeftKeyKeeper")
                {
                    anim.Stop("blueTriggerBlink");
                    anim.Play("blueTriggerDissapear");
                }
                if (gameObject.name == "RightKeyKeeper")
                {
                    anim.Stop("orangeTriggerBlink");
                    anim.Play("orangeTriggerDissapear");
                }
            } 
        }
    }
}
