using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedMovement : MonoBehaviour {
    public int type;
    public int direction;
    
    private PlayerControllerScript playerScript;
    private Rigidbody2D rb;
    private Vector2 initialPosition;
    private int t;

    // Start is called before the first frame update
    void Start() {
        t = 0;
        rb = this.gameObject.GetComponentInChildren<Rigidbody2D>();
        this.initialPosition = this.rb.position;
        this.playerScript = GameObject.Find("Player").gameObject.GetComponent<PlayerControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.type) {
            case 0:
                MovementType1();
                break;
        }
    }

    private void MovementType1() {
        this.rb.velocity = 100 * (MovementType1Func(this.t) - MovementType1Func(t-1));
        this.t++;
    }

    private Vector2 MovementType1Func(int t) {
        return new Vector2(-.2f * (1/60.0f * t - 3) * (1/60.0f * t - 3)  + 5,.2f * (3 - 1/60.0f*t));
    }

    public void OnTriggerEnter2D(Collider2D other) {
        this.playerScript.seedExists = false;
        Destroy(this);
    }
}
