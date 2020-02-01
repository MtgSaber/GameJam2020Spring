using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    public GameObject player;
    private PlayerControllerScript pcs;
    bool future;
    // Start is called before the first frame update
    void Start()
    {
        future = false;
        pcs = player.GetComponent<PlayerControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        future = pcs.GetFuture();
        //Debug.Log("Future is " + future);
        if (future)
        {
            this.transform.position= new Vector3(0.0f,20.0f,-10.0f );
        }
        else if(!future)
        {
            this.transform.position= new Vector3(0.0f, 0.0f, -10.0f);
        }
    }
}
