using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCheckScript : MonoBehaviour
{
    bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.gameObject.tag == "Ground")|| (collision.collider.gameObject.tag == "Yarn"))
        {
            grounded = true;
            //Debug.Log(collision.collider.tag);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.collider.gameObject.tag == "Ground")||(collision.collider.gameObject.tag == "Yarn"))
        {
            grounded = false;
            //Debug.Log(collision.collider.tag);
        }
    }
    public bool GetGrounded()
    {
        return grounded;
    }
}
