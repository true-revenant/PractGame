using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHallController : MonoBehaviour
{
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    // Update is called once per frame
    private void Update()
    {
        // если все враги в главном зале убиты
        if (gameObject.transform.childCount == 0)
        {
            leftButton.SideDoorButtonIsAvailable = true;
            rightButton.SideDoorButtonIsAvailable = true;
        }
    }
}
