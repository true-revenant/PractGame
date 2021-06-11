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
        // ���� ��� ����� � ������� ���� �����
        if (gameObject.transform.childCount == 0)
        {
            leftButton.SideDoorButtonIsAvailable = true;
            rightButton.SideDoorButtonIsAvailable = true;
        }
    }
}
