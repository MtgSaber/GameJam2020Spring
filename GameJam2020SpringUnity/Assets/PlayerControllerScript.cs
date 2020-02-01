using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private float hMove;
    private float vMove;
    private Vector2 movement;
    private bool canMove;
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
        movement = new Vector2(hMove, vMove) * speed;

        Move();
    }

    void Move()
    {
        if (canMove)
        {
            rb.AddForce(movement);
        }
    }
}
