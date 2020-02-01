using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private float hMove;
    private Vector2 movement;
    private Vector2 jumpVector;
    private bool canMove;
    private Rigidbody2D rb;
    public float speed;
    private bool grounded;
    private bool jumping;
    private FootCheckScript feet;
    private bool future;
    bool canShift;
    bool gigawatts;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        grounded = false;
        jumping = false;
        future = false;
        canShift = true;
        gigawatts = false;
        rb = this.gameObject.GetComponentInChildren<Rigidbody2D>();
        feet = this.gameObject.GetComponentInChildren<FootCheckScript>();
       
    }

    // Update is called once per frame
    void Update()
    {

        hMove = Input.GetAxis("Horizontal");
        movement = new Vector2(hMove, 0.0f) * speed;

        if (Input.GetKeyDown(KeyCode.E))
        {
            gigawatts = true;
        }
        else
        {
            gigawatts = false;
        }

        //jumping control block
        grounded = feet.GetGrounded();
        Debug.Log(grounded);
        if (Input.GetKey(KeyCode.Space))
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }
        //apply movement
        Move();
    }

    void Move()
    {
        if (canMove)
        {
            rb.AddForce(movement);
            if (jumping && grounded)
            {
                Jump();
            }

            if (canShift&&gigawatts)
            {
                QuantumLeap();
            }
        }
    }
    void Jump()
    {
        jumpVector = new Vector2(0.0f, 100.0f);
        rb.AddForce(jumpVector);
    }
    void QuantumLeap()
    {
        if (!future)
        {
            this.gameObject.transform.Translate(0.0f, 20.0f, 0.0f);
            future = true;
        }
        else
        {
            this.gameObject.transform.Translate(0.0f, -20.0f, 0.0f);
            future = false;
        }
    }

    
    
}
