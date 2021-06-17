using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType kType;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();

            switch(kType)
            {
                case KeyType.blueKey:
                    player.blueKeyCollected = true;
                    break;
                case KeyType.orangeKey:
                    player.orangeKeyCollected = true;
                    break;
            }

            DestroyKey();
        }
    }

    private void DestroyKey()
    {
        Destroy(gameObject);
    }

}

public enum KeyType
{
    blueKey,
    orangeKey
}
