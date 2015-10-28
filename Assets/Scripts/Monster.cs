using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

    public float walkSpeed = 3;
    bool _isGrounded = true;

    Animator animator;

    const int STATE_IDILE = 0;
    const int STATE_DIE = 1;


    int _currentAnimationState = STATE_IDILE;

    string _currentDirection = "right";
    public float dir = 1;

    bool _isDead = false;
    Rigidbody2D _monsterComponent;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        _monsterComponent = GetComponent<Rigidbody2D>();
        changeDirection("left");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (_isDead == false) {
            _monsterComponent.velocity = new Vector2(2 * dir, _monsterComponent.velocity.y);
        }
    }


    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.name == "Background") {
            _isGrounded = true;
        }
        else if(coll.gameObject.name == "Player" || coll.gameObject.tag == "Wall") {
            if (_currentDirection == "left") {
                changeDirection("right");
            }
            else {
                changeDirection("left");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        changeState(STATE_DIE);
        _isDead = true;
        GameObject Go = GameObject.Find("Monster");

        Destroy(Go, 0.5f);
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

    void changeState(int state) {
        if (_currentAnimationState == state) {
            return;
        }

        animator.SetInteger("state_monster", state);

        _currentAnimationState = state;
    }

    public float getDirection() {
        return dir;
    }

}
