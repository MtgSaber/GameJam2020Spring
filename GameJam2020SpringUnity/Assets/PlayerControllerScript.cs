using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private float hMove;
    private float vMove;
    private Vector2 movement;
    private bool canMove;
    private bool isGrounded;
    private Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        rb = this.gameObject.GetComponentInChildren<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxis("Horizontal");
        vMove = Input.GetAxis("Vertical");
        movement = new Vector2(hMove, (this.isGrounded ? this.vMove : 0)) * speed;

        Move();
    }

    void Move()
    {
        if (canMove) {
            Vector2 velocity = this.rb.velocity;
            float projectedX = velocity.x + this.movement.x;
            float projectedY = velocity.y + this.movement.y;
            
            /*
            Debug.Log(
                "Movement: {"
                + "movement: " + this.movement.ToString()
                + ", velocity: " + velocity.ToString()
                + ", projectedX: " + projectedX
                + ", projectedY" + projectedY
                + "}"
            );
            */
            
            if (Math.Abs(projectedX) <= Math.Abs(this.speed))
                velocity.x += this.movement.x;
            if (Math.Abs(projectedY) <= Math.Abs(this.speed))
                velocity.y += this.movement.y;

            this.rb.velocity = velocity;
        }
    }
}
