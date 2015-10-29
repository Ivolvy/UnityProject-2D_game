using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target; //the player
    private Vector3 offset;
    Vector3 targetPos;

    // Use this for initialization
    void Start () {
        targetPos = transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Debug.Log(target.transform.position.x);

        if (target && target.transform.position.x >= -5.4 && target.transform.position.x <= 20.6) {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;



            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 20f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            if (target.transform.position.y > -2.9f) {
                targetPos.y = target.transform.position.y;
            }
            //if the y position of player doesn't rises, fixe the camera
            else {
                targetPos.y = -2.9f;
            }

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
            
        }
	}
}
