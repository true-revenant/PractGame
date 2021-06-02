using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public ButtonType buttonType;
    
    [SerializeField] private Door _door;
    private Vector3 pressedButtonPos;
    private bool buttonPressed = false;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter() Called!");

        switch(buttonType)
        {
            case ButtonType.firstButton:
                if (other.CompareTag("Player"))
                {
                    _door.doorIsOpening = true;
                    buttonPressed = true;
                }
                break;

            case ButtonType.leftSectorButton:
                break;

            case ButtonType.rightSectorButton:
                break;
        }
    }

    private void Start()
    {
        pressedButtonPos = new Vector3(transform.GetChild(1).localPosition.x, transform.GetChild(1).localPosition.y, transform.GetChild(1).localPosition.z - 0.05f);
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
    leftSectorButton,
    rightSectorButton
}
