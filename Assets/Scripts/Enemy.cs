using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        //gameObject.SetActive(false);
        //Debug.Log($"Enemy Tag = {gameObject.tag}");

        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        var playerGO = GameObject.Find("Player");
        if (playerGO != null)
        {
            Debug.Log("Player is found!");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void TakeDamage()
    {
        Debug.Log($"{name} : OUCH!!!");
    }
}
