using UnityEngine;
using System.Collections;

public class Monster : ACharacter {

    public float walkSpeed = 3;
    bool _isGrounded = true;

    Animator animator;

    string _currentDirection = "right";
    public float dir = 1;

    bool _isDead = false;
    Rigidbody2D _monsterComponent;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        _monsterComponent = GetComponent<Rigidbody2D>();
        changeState("WALK", animator);
        changeMonsterDirection("left");
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
                changeMonsterDirection("right");
            }
            else {
                changeMonsterDirection("left");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        changeState("DIE", animator);
        _isDead = true;
        GameObject Go = GameObject.Find("Monster");

        Destroy(Go, 0.5f);
    }


    public void changeMonsterDirection(string direction) {
        _currentDirection = base.changeDirection(direction);

        if (_currentDirection == "right") {
            dir = 1;
        }
        else if (_currentDirection == "left") {
            dir = -1;
        }
    }


    public float getDirection() {
        return dir;
    }

}
