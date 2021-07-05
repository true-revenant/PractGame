using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public ButtonType buttonType;
    
    [SerializeField] private Door _door;
    private Vector3 pressedButtonPos;
    private bool buttonPressed = false;
    private NotificationManager notificationManager;

    public bool SideDoorButtonIsAvailable { get; set; } = false;

    private void Awake()
    {
        notificationManager = GetComponent<NotificationManager>();
        pressedButtonPos = new Vector3(transform.GetChild(1).localPosition.x, transform.GetChild(1).localPosition.y, transform.GetChild(1).localPosition.z - 0.05f);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(buttonType)
        {
            case ButtonType.firstButton:
                if (other.CompareTag("Player"))
                {
                    _door.doorIsOpening = true;
                    buttonPressed = true;
                }
                break;

            case ButtonType.sideSectorButton:
                if (other.CompareTag("Player"))
                {
                    if (SideDoorButtonIsAvailable)
                    {
                        _door.doorIsOpening = true;
                        buttonPressed = true;
                    }
                    else notificationManager.ShowNotification("В комнате еще есть враги!");
                }
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !SideDoorButtonIsAvailable && buttonType == ButtonType.sideSectorButton) 
            notificationManager.HideNotification();
    }

    // Update is called once per frame
    void Update() 
    {
        if (buttonPressed) PressButton();
    }

    private void PressButton()
    {
        transform.GetChild(1).localPosition = Vector3.MoveTowards(transform.GetChild(1).localPosition, pressedButtonPos, 5f * Time.deltaTime);
        transform.GetChild(1).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ButtonPressed");
    }
}

public enum ButtonType
{
    firstButton,
    sideSectorButton,
}
