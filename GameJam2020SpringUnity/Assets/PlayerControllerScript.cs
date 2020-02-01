using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControllerScript : MonoBehaviour
{
    private float hMove;
    private Vector2 movement;
    private Vector2 jumpVector;
    private bool canMove;
    private Rigidbody2D rb;
    [FormerlySerializedAs("speed")] public float hSpeed;
    public float vSpeed;
    private bool grounded;
    private FootCheckScript feet;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        grounded = false;
        rb = this.gameObject.GetComponentInChildren<Rigidbody2D>();
        feet = this.gameObject.GetComponentInChildren<FootCheckScript>();
       
    }

    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxis("Horizontal");
        movement = new Vector2(hMove, 0.0f) * this.hSpeed;

        grounded = feet.GetGrounded();
        //Debug.Log(grounded);
        if (Input.GetKey(KeyCode.Space)) {
            this.movement.y = (this.grounded ? this.vSpeed : 0.0f);
        }

        Move();
    }

    void Move()
    {
        if (canMove)
        {
            Vector2 velocity = this.rb.velocity;
            float attemptedX = velocity.x + this.movement.x;
            float attemptedY = velocity.y + this.movement.y;
            
            
            Debug.Log(
                "Movement: {"
                + "movement: " + this.movement.ToString()
                + ", velocity: " + velocity.ToString()
                + ", projectedX: " + attemptedX
                + ", projectedY: " + attemptedY
                + ", grounded: " + this.grounded
                + "}"
            );
            
            
            if (Math.Abs(attemptedX) <= Math.Abs(this.hSpeed))
                velocity.x += this.movement.x;
            if (Math.Abs(attemptedY) <= Math.Abs(this.vSpeed))
                velocity.y += this.movement.y;

            this.rb.velocity = velocity;
        }
    }

    
    
}
