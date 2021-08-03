using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class MainHallController : MonoBehaviour
{
    [SerializeField] private DoorButton leftButton;
    [SerializeField] private DoorButton rightButton;

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
