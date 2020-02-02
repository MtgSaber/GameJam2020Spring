using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject pastAnchorsContainer;
    public GameObject futureAnhorContainer;
    private CameraRefScript[] pastAnchors;
    private CameraRefScript[] futureAnchors;
    private PlayerControllerScript pcs;
    bool future;
    public int currentRoom;
    private Transform destination;
    private int lastKnownRoom;
    // Start is called before the first frame update
    void Start()
    {
        future = false;
        currentRoom = 0;
        lastKnownRoom = currentRoom;
        pcs = player.GetComponent<PlayerControllerScript>();
        pastAnchors = pastAnchorsContainer.GetComponentsInChildren<CameraRefScript>();
        futureAnchors = futureAnhorContainer.GetComponentsInChildren<CameraRefScript>();
    }

    // Update is called once per frame
    void Update()
    {
        future = pcs.GetFuture();
        currentRoom = pcs.GetRoom();
        if (!future)
        {
            destination = pastAnchors[currentRoom].transform;
        }
        else if (future)
        {
            destination = futureAnchors[currentRoom].transform;
        }
        if (pcs.canMove)
        {
            MoveToAnchor(currentRoom);
        }
        if (lastKnownRoom != currentRoom)
        {
            float d = destination.transform.position.x - this.transform.position.x;
            if (pcs.GetMove())
            {
                
            }
            else
            {
                if (d > .01)
                {

                    this.transform.Translate(8 * Time.deltaTime, 0.0f, 0.0f, Space.World);
                }
                else
                {
                    lastKnownRoom = currentRoom;
                    pcs.SetMove(true);
                }
            }
            
        }
        //Debug.Log("Future is " + future);
        //Debug.Log(pastAnchors[currentRoom].gameObject.transform);

        

        
        
    }

    void MoveToAnchor(int roomNumber)
    {
        if (future)
        {
            this.transform.position = destination.position;
        }
        else if (!future)
        {
            this.transform.position = destination.position;
        }
    }
}
