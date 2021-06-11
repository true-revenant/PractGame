using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideHallController : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private GameObject keyKeeper;
    
    private bool enemiesInLeftSectorKilled = false;
    private bool enemiesInRightSectorKilled = false;

    // Update is called once per frame
    void Update()
    {
        // если все враги в зале убиты
        if (gameObject.transform.childCount == 0)
        {
            door.doorIsOpening = true;
            if (keyKeeper != null)
            {
                Debug.Log("ВРАГИ УБИТЫ!");
                if (!enemiesInLeftSectorKilled && keyKeeper.name == "LeftKeyKeeper")
                {
                    keyKeeper.GetComponent<Animation>().Play("blueTriggerBlink");
                    enemiesInLeftSectorKilled = true;
                }
                if (!enemiesInRightSectorKilled && keyKeeper.name == "RightKeyKeeper")
                {
                    keyKeeper.GetComponent<Animation>().Play("orangeTriggerBlink");
                    enemiesInRightSectorKilled = true;
                }
            }
        }
    }
}
