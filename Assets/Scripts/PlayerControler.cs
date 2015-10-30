using UnityEngine;
using System.Collections;

public class PlayerControler : ACharacter {

    public float walkSpeed = 3;

    bool _isGrounded = true;
    bool _pauseMovement = false;


    Animator animator;


    float _timeHeld = 0f;
    float _heightJump = 5f;
    bool _addedMaxJump = false;

    bool _idile;
    bool _walk;
    bool _jump;
    bool _duck;
    bool _hurt;

    bool _soundJumpPlaying = false;

    Rigidbody2D _playerComponent;
    GameObject GoMonster;
    Monster monster;

    public AudioClip jump;
    public AudioClip hurt;
    public AudioClip walk;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        _playerComponent = GetComponent<Rigidbody2D>();

        //get the monster object
        GoMonster = GameObject.Find("Monster");
        monster = (Monster)GoMonster.GetComponent(typeof(Monster));

        audioSource = GetComponent<AudioSource>();
    }


    void Update() {

        //Manage the sound when the player walks
        if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && _isGrounded) {
            if(_soundJumpPlaying == false) {
                _soundJumpPlaying = true;
                audioSource.loop = true;
                audioSource.clip = walk;
                audioSource.Play();
            }
        }
        else if ((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) || !_isGrounded) {
            if (_soundJumpPlaying == true) {
                _soundJumpPlaying = false;
                audioSource.Stop();
            }
        }
    }


    void FixedUpdate () {

        //Jump functionality for the player
        //if we hold the space key more time, the player go highter
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (_isGrounded) {
                Jump = true;
                _isGrounded = false;
                _heightJump = 5f;

                _playerComponent.AddForce(new Vector2(0, 250));
                audioSource.PlayOneShot(jump);
            }
        }
        if (Input.GetKey(KeyCode.Space)) {
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

            if (Input.GetKey(KeyCode.LeftArrow) && !Duck) {
                changeDirection("left");
                _playerComponent.velocity = new Vector2(-3, _playerComponent.velocity.y);

                if (_isGrounded) {
                    Walk = true;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !Duck) {
                changeDirection("right");

                _playerComponent.velocity = new Vector2(3, _playerComponent.velocity.y);

                if (_isGrounded) {
                    Walk = true;
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow)) {
                if (_isGrounded) {
                    Duck = true;
                }
            }
            else if (_isGrounded) {
                Idile = true;
                _playerComponent.velocity = new Vector2(_playerComponent.velocity.x / 1.05f, _playerComponent.velocity.y);
            }
        }
    }


    void OnCollisionEnter2D(Collision2D coll){

        if((coll.gameObject.name == "Background" || coll.gameObject.name == "Platform") && !Hurt) {
            _isGrounded = true;
            Idile = true;
        }
        else if(coll.gameObject.name == "Monster") {
            if (_isGrounded) {
                float monsterDirection = monster.getDirection();

                Hurt = true;
                
                audioSource.PlayOneShot(hurt);

                //push the player on the opposate direction of the monster
                GetComponent<Rigidbody2D>().velocity = new Vector2(2 * monsterDirection, 2.5f);
                _pauseMovement = true;
                Invoke("enableMovement", 2);
            }
            else { //if the player jump on the mosnter's head
                GetComponent<Rigidbody2D>().velocity = new Vector2(_playerComponent.velocity.x, 2.5f);
            }
        }
    }


    //enable the movement of the Player after is was hurted
    void enableMovement() {
        _pauseMovement = false;
        Idile = true;
    }


    //GETTERS AND SETTERS

    public bool Idile {
        get {
            return _idile;
        }
        set {
            _idile = value;
            _hurt = false;
            _duck = false;
            changeState("IDILE", animator);
        }
    }

    public bool Walk {
        get {
            return _walk;
        }
        set {
            _walk = value;
            _duck = false;
            changeState("WALK", animator);
        }
    }

    public bool Jump {
        get {
            return _jump;
        }
        set {
            _jump = value;
            changeState("JUMP", animator);
        }
    }

    public bool Duck {
        get {
            return _duck;
        }
        set {
            _duck = value;
            changeState("DUCK", animator);
        }
    }

    public bool Hurt {
        get {
            return _hurt;
        }
        set {
            _hurt = value;
            changeState("HURT", animator);
        }
    }
    
}
