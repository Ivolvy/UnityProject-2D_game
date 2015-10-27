using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {

    public float walkSpeed = 3;

    bool _isGrounded = true;



    Animator animator;

    const int STATE_IDILE = 0;
    const int STATE_WALK = 1;
    const int STATE_JUMP = 2;
    const int STATE_DUCK = 3;


    string _currentDirection = "right";
    int _currentAnimationState = STATE_IDILE;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
    }
	

	void FixedUpdate () {


       
        if (Input.GetKey(KeyCode.LeftArrow)) {
            changeDirection("left");
            GetComponent<Rigidbody2D>().velocity = new Vector2(-3, GetComponent<Rigidbody2D>().velocity.y);

            if (_isGrounded) {
                changeState(STATE_WALK);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                if (_isGrounded) {
                    _isGrounded = false;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 6);
                    changeState(STATE_JUMP);
                }
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            changeDirection("right");
            //transform.Translate(Vector3.right * walkSpeed * Time.fixedDeltaTime);


            GetComponent<Rigidbody2D>().velocity = new Vector2(3, GetComponent<Rigidbody2D>().velocity.y);


            if (_isGrounded) {
                changeState(STATE_WALK);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                if (_isGrounded) {
                    _isGrounded = false;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 6);
                    changeState(STATE_JUMP);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (_isGrounded) {
                _isGrounded = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 6);
                changeState(STATE_JUMP);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (_isGrounded) {
                changeState(STATE_DUCK);
            }
        }
        else if(_isGrounded) {
            changeState(STATE_IDILE);
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x / 1.05f , GetComponent<Rigidbody2D>().velocity.y);
        }



    }

    void changeState(int state){
        if(_currentAnimationState == state){
            return;
        }

        animator.SetInteger("state", state);

        _currentAnimationState = state;
    }

    void OnCollisionEnter2D(Collision2D coll){
        
        if(coll.gameObject.name == "Background") {
            _isGrounded = true;
            changeState(STATE_IDILE);
        }
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
