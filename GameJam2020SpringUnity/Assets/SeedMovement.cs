﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedMovement : MonoBehaviour {
    public int type;

    private PlantManagerScript plantManagerScript;
    private PlayerControllerScript playerScript;
    private int direction;
    private Rigidbody2D rb;
    private Vector2 initialPosition;
    private int t;

    // Start is called before the first frame update
    void Start() {
        t = 0;
        rb = this.gameObject.GetComponentInChildren<Rigidbody2D>();
        this.initialPosition = this.rb.position;
        this.playerScript = GameObject.Find("Player").gameObject.GetComponent<PlayerControllerScript>();
        this.direction = this.playerScript.direction;
        if (this.type != 1) {
            this.rb.gravityScale = 1f;
            this.rb.velocity = new Vector2(this.direction, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (t > 60 * 10) {
            this.playerScript.seedExists = false;
            Destroy(gameObject);
        }
        if (this.type == 1)
            MovementType1();
    }

    private void MovementType1() {
        this.rb.velocity = 100 * (MovementType1Func(this.t) - MovementType1Func(t-1));
        t++;
    }

    private Vector2 MovementType1Func(int t) {
        return new Vector2(-this.direction * .1f * (1/15.0f * t - 3) * (1/15.0f * t - 3)  + 5,.2f * (3 - 1/15.0f*t));
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Dirt")) {
            switch (this.type) {
                case 0:
                    Instantiate(this.plantManagerScript.tree, this.rb.position + new Vector2(0, 20f), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(this.plantManagerScript.mushroom, this.rb.position + new Vector2(0, 20f), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(this.plantManagerScript.vine, this.rb.position + new Vector2(0, 20f), Quaternion.identity);
                    break;
            }
        }
        
        if (!other.gameObject.tag.Equals("Player")) {
            this.playerScript.seedExists = false;
            Destroy(this.gameObject);
        }
    }

    public void setDirection(int direction) {
        this.direction = direction;
    }

    public int getDirection() {
        return this.direction;
    }
}
