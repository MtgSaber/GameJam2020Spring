using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRefScript : MonoBehaviour
{
    public bool future;
    public Transform anchorPoint;
    public bool alternateSize;
    // Start is called before the first frame update
    void Start()
    {
        anchorPoint = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
