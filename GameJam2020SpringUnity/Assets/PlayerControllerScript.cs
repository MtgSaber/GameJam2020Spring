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
    private bool future;
    bool canShift;
    bool gigawatts;
    public int direction;
    public GameObject seedObj;
    bool seedExists;
    private int currentRoom;

    
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        grounded = false;
        future = false;
        canShift = true;
        gigawatts = false;
        rb = this.gameObject.GetComponentInChildren<Rigidbody2D>();
        feet = this.gameObject.GetComponentInChildren<FootCheckScript>();
        this.seedExists = false;
    }

    // Update is called once per frame
    void Update()
    {

        hMove = Input.GetAxis("Horizontal");
        movement = new Vector2(hMove, 0.0f) * this.hSpeed;
        this.direction = Math.Sign(this.hMove);
        
        Vector3 pos = this.rb.position;
        
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
        //Debug.Log(grounded);
        if (Input.GetKey(KeyCode.Space)) {
            this.movement.y = (this.grounded ? this.vSpeed : 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Q) && !this.seedExists) {
            Debug.Log("pressed q");
            Instantiate(this.seedObj, pos + new Vector3(this.direction*.5f, 0, 0), Quaternion.identity);
        }
        
        //apply movement
        Move();
    }

    void Move()
    {
        if (canMove)
        {
            Vector2 velocity = this.rb.velocity;
            float attemptedX = velocity.x + this.movement.x;
            float attemptedY = velocity.y + this.movement.y;
            
            if (Math.Abs(velocity.x) < this.vSpeed || Math.Sign(this.movement.x) != Math.Sign(velocity.x))
                if (Math.Abs(attemptedX) < this.hSpeed)
                    velocity.x += this.movement.x;
                else
                    velocity.x = this.vSpeed * Math.Sign(this.movement.x);
            if (Math.Abs(velocity.y) < this.vSpeed || Math.Sign(this.movement.y) != Math.Sign(velocity.y))
                if (Math.Abs(attemptedY) < this.vSpeed)
                    velocity.y += this.movement.y;
                else
                    velocity.y = this.vSpeed * Math.Sign(this.movement.y);

            this.rb.velocity = velocity;
        
            

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

    public bool GetFuture()
    {
        return future;
    }

    public int GetRoom()
    {
        return currentRoom;
    }

    public void SetRoom(int num)
    {
        currentRoom = num;
    }
    
    
}
