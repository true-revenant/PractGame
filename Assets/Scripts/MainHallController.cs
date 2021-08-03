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
        // ���� ��� ����� � ������� ���� �����
        if (gameObject.transform.childCount == 0)
        {
            leftButton.SideDoorButtonIsAvailable = true;
            rightButton.SideDoorButtonIsAvailable = true;
        }
    }
}
