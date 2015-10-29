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


    public AudioClip jump;
    public AudioClip hurt;
    public AudioClip walk;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        _playerComponent = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update() {

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
                _isGrounded = false;
                _heightJump = 5f;

                _playerComponent.AddForce(new Vector2(0, 250));
                audioSource.PlayOneShot(jump);
            }
        }
        if (Input.GetKey(KeyCode.Space)) {
            Jump = true;
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
                    Walk = true;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow)) {
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
        
        if(coll.gameObject.name == "Background" || coll.gameObject.name == "Platform") {
            _isGrounded = true;
            Idile = true;
        }
        else if(coll.gameObject.name == "Monster") {

            //get the monster object
            GameObject Go = GameObject.Find("Monster");
            Monster monster = (Monster)Go.GetComponent(typeof(Monster));
            float monsterDirection = monster.getDirection();

            Hurt = true;
            audioSource.PlayOneShot(hurt);

            //push the player on the opposate direction of the monster
            GetComponent<Rigidbody2D>().velocity = new Vector2(2 * monsterDirection, 2.5f);
            _pauseMovement = true;
            Invoke("enableMovement", 2);
        }
    }


    //enable the movement of the Player
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
            _idile = true;
            changeState("IDILE", animator);
        }
    }

    public bool Walk {
        get {
            return _walk;
        }
        set {
            _walk = true;
            changeState("WALK", animator);
        }
    }

    public bool Jump {
        get {
            return _jump;
        }
        set {
            _jump = true;
            changeState("JUMP", animator);
        }
    }

    public bool Duck {
        get {
            return _duck;
        }
        set {
            _duck = true;
            changeState("DUCK", animator);
        }
    }

    public bool Hurt {
        get {
            return _hurt;
        }
        set {
            _hurt = true;
            changeState("HURT", animator);
        }
    }
    
}
