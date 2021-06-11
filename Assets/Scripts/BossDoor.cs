using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public bool blueKeyIsInserted { get; set; }
    public bool orangeKeyIsInserted { get; set; }

    private Vector3 openedPos;
    private Vector3 closedPos;

    // Start is called before the first frame update
    void Start()
    {
        openedPos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        closedPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (blueKeyIsInserted && orangeKeyIsInserted) OpenDoor();
    }
    private void OpenDoor()
    {
        transform.position = Vector3.MoveTowards(transform.position, openedPos, 10f * Time.deltaTime);
    }
    public void CloseDoor()
    {
        transform.position = Vector3.MoveTowards(transform.position, closedPos, 10f * Time.deltaTime);
    }
}
