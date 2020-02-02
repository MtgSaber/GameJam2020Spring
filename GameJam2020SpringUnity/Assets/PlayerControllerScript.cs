using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


    public class PlayerControllerScript : MonoBehaviour
    {

        AudioSource[] beepAndBoop;
        public AudioClip[] sounds;
        public AudioClip[] songs;
        Animator anim;
        public float yarnOffset;
        public float vSpeed;
        public int direction;
        [FormerlySerializedAs("seedObj")] public GameObject seed1Obj;
        public GameObject seed2Obj;
        public GameObject seed3Obj;
        public GameObject yarn;
        public bool seedExists;
        public float offsetX;
        [FormerlySerializedAs("speed")] public float hSpeed;
        public int currentRoom;
        public bool canMove;

        private float hMove;
        private Vector2 movement;
        private Vector2 jumpVector;
        private Vector3 lastStarSpot;
        private Rigidbody2D rb;
        private bool grounded;
        private FootCheckScript feet;
        private bool future;
        private bool canShift;
        private bool gigawatts;
        private PlantManagerScript plantManagerScript;
        private GameObject characterSprite;
        int songNum;


        // Start is called before the first frame update
        void Start()
        {
        System.Random r;
        r = new System.Random();
        songNum = r.Next(0, 2);
        Debug.Log(songNum);
            beepAndBoop = this.gameObject.GetComponents<AudioSource>();
            lastStarSpot = this.gameObject.transform.position;
            canMove = true;
            grounded = false;
            future = false;
            canShift = true;
            gigawatts = false;
            rb = this.gameObject.GetComponentInChildren<Rigidbody2D>();
            feet = this.gameObject.GetComponentInChildren<FootCheckScript>();
            this.seedExists = false;
            currentRoom = 0;
            this.plantManagerScript = GameObject.Find("PlantManager").GetComponent<PlantManagerScript>();
            anim = this.gameObject.GetComponentInChildren<Animator>();
            characterSprite = GameObject.Find("Protaginist_Idle");
        beepAndBoop[0].clip = songs[songNum];
        beepAndBoop[0].Play();
    }

        // Update is called once per frame
        void Update()
        {



            Vector2 pos = this.gameObject.transform.position;

            if (!this.seedExists && !this.future)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Instantiate(this.seed1Obj, pos + new Vector2(this.direction * this.offsetX, .7f), Quaternion.identity);
                    this.seedExists = true;
                    this.rb.velocity += new Vector2(-2 * this.hSpeed * this.direction, .5f);

                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    Instantiate(this.seed2Obj, pos + new Vector2(this.direction * this.offsetX, .7f), Quaternion.identity);
                    this.seedExists = true;
                    this.rb.velocity += new Vector2(-2 * this.hSpeed * this.direction, .5f);

                }
                else if (Input.GetKeyDown(KeyCode.C))
                {
                    Instantiate(this.seed3Obj, pos + new Vector2(this.direction * this.offsetX, .7f), Quaternion.identity);
                    this.seedExists = true;
                    this.rb.velocity += new Vector2(-2 * this.hSpeed * this.direction, .5f);
                }
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                BiteZaDusto();
            }


            hMove = Input.GetAxis("Horizontal");
            movement = new Vector2(hMove, 0.0f) * this.hSpeed;
            if (this.hMove != 0) this.direction = Math.Sign(this.hMove);

            //jumping control block
            grounded = feet.GetGrounded();
            gigawatts = Input.GetKeyDown(KeyCode.E);

            //Debug.Log(grounded);
            if (Input.GetKey(KeyCode.Space)) this.movement.y = (this.grounded ? this.vSpeed : 0.0f);

            //apply movement
            Move();

            bool g = grounded;
            bool v;
            if (rb.velocity.magnitude > 0)
            {
                v = true;
            }
            else
            {
                v = false;
            }
            if (direction < 0)
            {
                characterSprite.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
            else if (direction > 0)
            {
                characterSprite.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
            anim.SetBool("IsMoving", v);
            anim.SetBool("isGrounded", g);

        }

        private void BiteZaDusto()
        {
            Rigidbody2D yarnRB = this.yarn.GetComponent<Rigidbody2D>();

            this.plantManagerScript.BiteZaDusto();

            this.rb.position = this.lastStarSpot + new Vector3(1, this.future ? PlantManagerScript.FUTURE_OFFSET : 0, 0);
            yarnRB.position = this.lastStarSpot + new Vector3(1 + this.yarnOffset, this.future ? PlantManagerScript.FUTURE_OFFSET : 0, 0);
            yarnRB.velocity = Vector3.zero;
            yarnRB.angularVelocity = 0;
            beepAndBoop[1].clip = sounds[0];
            beepAndBoop[1].Play();
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
                if (movement.y != 0 && (Math.Abs(velocity.y) < this.vSpeed || Math.Sign(this.movement.y) != Math.Sign(velocity.y)))
                    if (Math.Abs(attemptedY) < this.vSpeed)
                        velocity.y += this.movement.y;
                    else
                        velocity.y = this.vSpeed * Math.Sign(this.movement.y);

                this.rb.velocity = velocity;

                if (canShift && gigawatts)
                {
                    QuantumLeap();
                }
            }
        }

        void QuantumLeap()
        {
        beepAndBoop[1].clip = sounds[0];
        beepAndBoop[1].Play();
            if (!future)
            {
                this.gameObject.transform.Translate(0.0f, 16.6f, 0.0f);
                yarn.transform.Translate(0.0f, 16.6f, 0.0f);
                future = true;
            }
            else
            {
                this.gameObject.transform.Translate(0.0f, -16.6f, 0.0f);
                yarn.transform.Translate(0.0f, -16.6f, 0.0f);
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

        public bool GetMove()
        {
            return canMove;
        }

        public void SetMove(bool val)
        {
            canMove = val;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Star")
            {
                SetMove(false);
                currentRoom++;
                lastStarSpot = collision.gameObject.transform.position;
                Destroy(collision.gameObject);
            }
        }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!grounded&&collision.collider.tag == "Ground")
        {
            beepAndBoop[1].clip = sounds[1];
            beepAndBoop[1].Play();
        }
    }
}