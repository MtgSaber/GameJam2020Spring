using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedScript : MonoBehaviour {
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
        this.plantManagerScript = GameObject.Find("PlantManager").GetComponent<PlantManagerScript>();
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
            this.plantManagerScript.CreatePlant(this.type, this.rb.position);
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
