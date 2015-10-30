using UnityEngine;
using System.Collections;

public class Monster : ACharacter {

    public float walkSpeed = 3;
    bool _isGrounded = true;

    Animator animator;

    string _currentDirectionMs = "right";
    public float dir = 1;

    bool _isDead = false;
    Rigidbody2D _monsterComponent;

    public AudioClip kill;
    AudioSource audioSource;


    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        _monsterComponent = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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
            if (_currentDirectionMs == "left") {
                changeMonsterDirection("right");
            }
            else {
                changeMonsterDirection("left");
            }
        }
    }

    //kill the monster when the player jump on it
    void OnTriggerEnter2D(Collider2D other) {
        changeState("DIE", animator);
        audioSource.PlayOneShot(kill);

        _isDead = true;
        Destroy(gameObject, 0.5f);
    }

    public void changeMonsterDirection(string direction) {
        _currentDirectionMs = base.changeDirection(direction);

        if (_currentDirectionMs == "right") {
            dir = 1;
        }
        else if (_currentDirectionMs == "left") {
            dir = -1;
        }
    }

    public float getDirection() {
        return dir;
    }

}