using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {

    public float walkSpeed = 3;

    bool _isGrounded = true;
    bool _pauseMovement = false;


    Animator animator;

    string _previousState = "";

    string _currentDirection = "right";
    string _currentAnimationState = "IDILE";


    float _timeHeld = 0f;
    float _heightJump = 5f;
    bool _addedMaxJump = false;

    Rigidbody2D _playerComponent;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        _playerComponent = GetComponent<Rigidbody2D>();
    }


    void Update() {



    }


    void FixedUpdate () {

        //Jump functionality for the player
        //if we hold the space key more time, the player go highter
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (_isGrounded) {
                _isGrounded = false;
                _heightJump = 5f;

                _playerComponent.AddForce(new Vector2(0, 250));
            }
        }
        if (Input.GetKey(KeyCode.Space)) {
            changeState("JUMP");
            _timeHeld += Time.deltaTime;
          
            if (_timeHeld > 0.2f && _addedMaxJump != true) {
                _addedMaxJump = true;
                _heightJump = 5f;
                _playerComponent.velocity = new Vector2(_playerComponent.velocity.x, _heightJump);
            }

        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            _timeHeld = 0;
            _addedMaxJump = false;
        }


        if (_pauseMovement != true) {

            if (Input.GetKey(KeyCode.LeftArrow)) {
                changeDirection("left");
                _playerComponent.velocity = new Vector2(-3, _playerComponent.velocity.y);

                if (_isGrounded) {
                    changeState("WALK");
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow)) {
                changeDirection("right");

                _playerComponent.velocity = new Vector2(3, _playerComponent.velocity.y);

                if (_isGrounded) {
                    changeState("WALK");
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                if (_isGrounded) {
                    changeState("DUCK");
                }
            }
            else if (_isGrounded) {
                changeState("IDILE");
                _playerComponent.velocity = new Vector2(_playerComponent.velocity.x / 1.05f, _playerComponent.velocity.y);
            }
        }
    }


    void changeState(string state){
        if(_currentAnimationState == state){
            return;
        }
        if (_previousState != "") {
            animator.SetBool(_previousState, false);
        }

        animator.SetBool(state, true);
        _currentAnimationState = state;

        _previousState = state;
    }

    void OnCollisionEnter2D(Collision2D coll){
        
        if(coll.gameObject.name == "Background" || coll.gameObject.name == "Platform") {
            _isGrounded = true;
            changeState("IDILE");
        }
        else if(coll.gameObject.name == "Monster") {

            //get the monster object
            GameObject Go = GameObject.Find("Monster");
            Monster monster = (Monster)Go.GetComponent(typeof(Monster));
            float monsterDirection = monster.getDirection();

            changeState("HURT");

            //push the player on the opposate direction of the monster
            GetComponent<Rigidbody2D>().velocity = new Vector2(2 * monsterDirection, 2.5f);
            _pauseMovement = true;
            Invoke("enableMovement", 2);
        }
    }


    //enable the movement of the Player
    void enableMovement() {
        _pauseMovement = false;
        changeState("IDILE");
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
