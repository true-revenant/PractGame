using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool doorIsOpening = false;

    [SerializeField] private DoorType doorType;

    private Vector3 simpleDoorOpenedPosition;
    private Vector3 simpleClosedDoorPosition;
    
    private Vector3 leftDoorOpenedPosition;
    private Vector3 rightDoorOpenedPosition;

    // Start is called before the first frame update
    private void Start() 
    {
        switch (doorType)
        {
            case DoorType.SimpleDoor:
                simpleDoorOpenedPosition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
                simpleClosedDoorPosition = transform.position;
                break;
            case DoorType.BossDoor:
                leftDoorOpenedPosition = new Vector3(transform.GetChild(0).localPosition.x + 6, transform.GetChild(0).localPosition.y, transform.GetChild(0).localPosition.z);
                rightDoorOpenedPosition = new Vector3(transform.GetChild(1).localPosition.x - 6, transform.GetChild(1).localPosition.y, transform.GetChild(1).localPosition.z);
                break;
        }
    }

    // Update is called once per frame
    private void Update() 
    {
        switch(doorType)
        {
            case DoorType.SimpleDoor:
                if (doorIsOpening) OpenSimpleDoor();
                else CloseDoor();
                break;
            case DoorType.BossDoor:
                if (doorIsOpening) OpenBossDoor();
                break;
        }
    }

    private void OpenSimpleDoor()
    {
        transform.position = Vector3.MoveTowards(transform.position, simpleDoorOpenedPosition, 10f * Time.deltaTime);
    }

    private void OpenBossDoor()
    {
        transform.GetChild(0).localPosition = Vector3.MoveTowards(transform.GetChild(0).localPosition, leftDoorOpenedPosition, 10f * Time.deltaTime);
        transform.GetChild(1).localPosition = Vector3.MoveTowards(transform.GetChild(1).localPosition, rightDoorOpenedPosition, 10f * Time.deltaTime);
    }

    private void CloseDoor()
    {
        transform.position = Vector3.MoveTowards(transform.position, simpleClosedDoorPosition, 10f * Time.deltaTime);
    }
}

public enum DoorType
{
    SimpleDoor,
    BossDoor
}