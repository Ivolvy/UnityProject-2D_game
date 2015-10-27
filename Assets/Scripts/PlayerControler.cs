﻿using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {

    public float walkSpeed = 3;

    bool _isGrounded = true;
    bool _pauseMovement = false;


    Animator animator;

    const int STATE_IDILE = 0;
    const int STATE_WALK = 1;
    const int STATE_JUMP = 2;
    const int STATE_DUCK = 3;
    const int STATE_HURT = 4;


    string _currentDirection = "right";
    int _currentAnimationState = STATE_IDILE;


    float _timeHeld = 0f;
    float _heightJump = 5f;
    bool _addedMaxJump = false;


    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
    }
	

	void FixedUpdate () {

        //Jump functionality for the player
        //if we hold the space key more time, the player go highter
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (_isGrounded) {
                _isGrounded = false;
                _heightJump = 5f;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, _heightJump);
            }
        }
        if (Input.GetKey(KeyCode.Space)) {
            changeState(STATE_JUMP);
            _timeHeld += Time.deltaTime;

            if (_timeHeld > 0.2f && _addedMaxJump != true) {
                _addedMaxJump = true;
                 _heightJump = 4.5f;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, _heightJump);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            _timeHeld = 0;
            _addedMaxJump = false;
        }


        if (_pauseMovement != true) {

            if (Input.GetKey(KeyCode.LeftArrow)) {
                changeDirection("left");
                GetComponent<Rigidbody2D>().velocity = new Vector2(-3, GetComponent<Rigidbody2D>().velocity.y);

                if (_isGrounded) {
                    changeState(STATE_WALK);
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow)) {
                changeDirection("right");
                //transform.Translate(Vector3.right * walkSpeed * Time.fixedDeltaTime);


                GetComponent<Rigidbody2D>().velocity = new Vector2(3, GetComponent<Rigidbody2D>().velocity.y);


                if (_isGrounded) {
                    changeState(STATE_WALK);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                if (_isGrounded) {
                    changeState(STATE_DUCK);
                }
            }
            else if (_isGrounded) {
                changeState(STATE_IDILE);
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x / 1.05f, GetComponent<Rigidbody2D>().velocity.y);
            }
        }


    }

    void jump() {
      
    }

    void changeState(int state){
        if(_currentAnimationState == state){
            return;
        }

        animator.SetInteger("state", state);

        _currentAnimationState = state;
    }

    void OnCollisionEnter2D(Collision2D coll){
        
        if(coll.gameObject.name == "Background" || coll.gameObject.name == "Platform") {
            _isGrounded = true;
            changeState(STATE_IDILE);
        }
        else if(coll.gameObject.name == "Monster") {

            //get the monster object
            GameObject Go = GameObject.Find("Monster");
            Monster monster = (Monster)Go.GetComponent(typeof(Monster));
            float monsterDirection = monster.getDirection();

            changeState(STATE_HURT);
            //push the player on the opposate direction of the monster
            GetComponent<Rigidbody2D>().velocity = new Vector2(2 * -monsterDirection, 2.5f);
            _pauseMovement = true;
            Invoke("enableMovement", 2);
        }
    }

    //enable the movement of the Player
    void enableMovement() {
        _pauseMovement = false;
        changeState(STATE_IDILE);
    }

    void changeDirection(string direction) {

        if(_currentDirection != direction) {
            if (direction == "right") {
                transform.Rotate(0, 180, 0);
            }
            else if(direction == "left"){
                transform.Rotate(0, -180, 0);
            }

            _currentDirection = direction;
        }
      
    }


}
