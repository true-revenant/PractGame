using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool doorIsOpening = false;

    private Vector3 openedPos;

    // Start is called before the first frame update
    private void Start() 
    {
        openedPos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        //Debug.Log($"DOOR TRANSFORM = ({transform.position.x}), ({transform.position.y}), ({transform.position.z})");
    }

    // Update is called once per frame
    private void Update() 
    {
        if (doorIsOpening) OpenDoor();
    }

    private void OpenDoor()
    {
        //Debug.Log("DOOR OPENED!");
        //transform.Translate(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z));
        transform.position = Vector3.MoveTowards(transform.position, openedPos, 10f * Time.deltaTime);
        //Debug.Log($"DOOR TRANSFORM = {transform.position.x}, {transform.position.y}, {transform.position.z}");
    }

}
