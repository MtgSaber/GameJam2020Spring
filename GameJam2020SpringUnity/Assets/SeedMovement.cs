using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedMovement : MonoBehaviour {
    public int type;
    public int direction;
    
    private PlayerControllerScript playerScript;
    private Rigidbody2D rb;
    private GameObject self;
    private Vector2 initialPosition;
    private int t;

    // Start is called before the first frame update
    void Start() {
        t = 0;
        rb = this.gameObject.GetComponentInChildren<Rigidbody2D>();
        this.initialPosition = this.rb.position;
        this.playerScript = GameObject.Find("Player").gameObject.GetComponent<PlayerControllerScript>();
        if (this.type != 1) {
            this.rb.gravityScale = 1f;
            this.rb.velocity = new Vector2(this.direction, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.type == 1)
            MovementType1();
    }

    private void MovementType1() {
        this.rb.velocity = 100 * (MovementType1Func(this.t) - MovementType1Func(t-1));
        this.t++;
    }

    private Vector2 MovementType1Func(int t) {
        return new Vector2(-this.direction * .3f * (1/60.0f * t - 3) * (1/60.0f * t - 3)  + 5,.3f * (3 - 1/60.0f*t));
    }

    public void OnTriggerEnter2D(Collider2D other) {
        this.playerScript.seedExists = false;
        Destroy(this.gameObject);
    }
}
