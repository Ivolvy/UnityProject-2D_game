using UnityEngine;
using System.Collections;

abstract public class ACharacter : MonoBehaviour {


    string _currentDirection = "right";

    string _currentAnimationState = "IDILE";
    string _previousState = "";

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }


    public void changeState(string state, Animator animator) {
        if (_currentAnimationState == state) {
            return;
        }
        if (_previousState != "") {
            animator.SetBool(_previousState, false);
        }

        animator.SetBool(state, true);
        _currentAnimationState = state;

        _previousState = state;
    }

    virtual public string changeDirection(string direction) {

        if (_currentDirection != direction) {
            if (direction == "right") {
                transform.Rotate(0, 180, 0);
            }
            else if (direction == "left") {
                transform.Rotate(0, -180, 0);
            }

            return _currentDirection = direction;
        }
        else {
            return direction;
        }
        
    }

}