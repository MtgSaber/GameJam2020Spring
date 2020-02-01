﻿using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
        future = false;
        currentRoom = 0;
        pcs = player.GetComponent<PlayerControllerScript>();
        pastAnchors = pastAnchorsContainer.GetComponentsInChildren<CameraRefScript>();
        futureAnchors = futureAnhorContainer.GetComponentsInChildren<CameraRefScript>();
    }

    // Update is called once per frame
    void Update()
    {
        future = pcs.GetFuture();
        currentRoom = pcs.GetRoom();
        //Debug.Log("Future is " + future);
        Debug.Log(pastAnchors[currentRoom].gameObject.transform);
        if (!future)
        {
            destination = pastAnchors[currentRoom].transform;
        }else if (future)
        {
            destination = futureAnchors[currentRoom].transform;
        }
        MoveToAnchor(currentRoom);
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
