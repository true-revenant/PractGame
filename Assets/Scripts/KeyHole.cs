using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHole : MonoBehaviour
{
    [SerializeField] private KeyHoleType KHtype;
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private BossDoor bossDoor;
    
    private bool keyIsInserted = false;
    private NotificationManager notificationManager;

    private void Awake()
    {
        notificationManager = GetComponent<NotificationManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !keyIsInserted)
        {
            switch(KHtype)
            {
                case KeyHoleType.blueHole:
                    if (other.GetComponent<PlayerController>().blueKeyCollected)
                    {
                        bossDoor.blueKeyIsInserted = true;
                        keyIsInserted = true;
                        ShowInsertedKey();
                    }
                    else notificationManager.ShowNotification("Нужен синий ключ!");
                    break;
                case KeyHoleType.orangeHole:
                    if (other.GetComponent<PlayerController>().orangeKeyCollected)
                    {
                        bossDoor.orangeKeyIsInserted = true;
                        keyIsInserted = true;
                        ShowInsertedKey();
                    }
                    else notificationManager.ShowNotification("Нужен оранжевый ключ!");
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !keyIsInserted)
        {
            notificationManager.HideNotification();
        }
    }

    private void ShowInsertedKey()
    {
        //var key = Instantiate(keyPrefab, new Vector3(-0.219f, 0.606f, -0.173f), Quaternion.Euler(0, -90, 0), transform);
        var key = Instantiate(keyPrefab, transform, false);
        key.transform.localPosition = new Vector3(-0.219f, 0.606f, -0.173f);
        key.transform.localRotation = Quaternion.Euler(0, -90, 0);
        key.GetComponent<Animation>().Stop();
        key.GetComponent<BoxCollider>().enabled = false;
    }
}

public enum KeyHoleType
{
    blueHole,
    orangeHole
}