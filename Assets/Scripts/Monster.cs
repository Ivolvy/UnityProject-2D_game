using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

    public float walkSpeed = 3;
    bool _isGrounded = true;

    Animator animator;

    string _currentDirection = "right";
    public float dir = 1;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        changeDirection("left");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //changeDirection("left");
        GetComponent<Rigidbody2D>().velocity = new Vector2(2* dir, GetComponent<Rigidbody2D>().velocity.y);
    }




    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.name == "Background") {
            _isGrounded = true;
        }
        else if(coll.gameObject.name == "Player" || coll.gameObject.tag == "Wall") {
            if(_currentDirection == "left") {
                changeDirection("right");
            }
            else {
                changeDirection("left");
            }
        }
    }

    void changeDirection(string direction) {

        if (_currentDirection != direction) {
            if (direction == "right") {
                transform.Rotate(0, 180, 0);
                dir = 1;
            }
            else if (direction == "left") {
                transform.Rotate(0, -180, 0);
                dir = -1;
            }

            _currentDirection = direction;
        }

    }

    public float getDirection() {
        return dir;
    }

}
