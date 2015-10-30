using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class laserScript : MonoBehaviour {
	public Transform startPoint; //satrt point of all the lasers
	//public Transform endPoint;
    public float timeInterval = 0.2f; //the interval between each laser beam
    public float laserWidth = 0.2f;
    public bool isRandom = false;

    LineRenderer laserLine;

    GameObject GoLasersEndPoints;

    int lastIndex = 0;
    int currentIndex = 0;
    int randomNumber;

    Transform[] lasersArray;


    // Use this for initialization
    void Start () {
		laserLine = GetComponentInChildren<LineRenderer> ();
		laserLine.SetWidth (laserWidth, laserWidth);
        GoLasersEndPoints = GameObject.Find("LasersEndPoints"); //get all the childs end lasers points
        lasersArray = new Transform[GoLasersEndPoints.transform.childCount];

        SetLasersEnd();
    }

    //set all the ending laser points
    void SetLasersEnd() {
        //rename all the chils laser points for more readability
        foreach (Transform child in GoLasersEndPoints.transform) {
            child.name = "LasersEnd-" + lastIndex;
            lasersArray[lastIndex] = child;
            lastIndex++;
        }
        if (isRandom) {
            StartCoroutine(TraceRandomLasers());
        }
        else {
            StartCoroutine(TraceLasers());
        }
    }

    //trace lasers by following the points in order
    IEnumerator TraceLasers() {
        for(var i = 0;i< lasersArray.Length; i++) {
            laserLine.SetPosition(0, startPoint.position);
            laserLine.SetPosition(1, lasersArray[i].position);
            yield return new WaitForSeconds(timeInterval);

            if (i == lasersArray.Length - 1) {
                StartCoroutine(TraceLasers());
            }
        }
    }

    //trace random lasers with the defined end points
    IEnumerator TraceRandomLasers() {
        randomNumber = Random.Range(0, lasersArray.Length);

        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, lasersArray[randomNumber].position);
        yield return new WaitForSeconds(timeInterval);

        StartCoroutine(TraceRandomLasers());
    }


    // Update is called once per frame
    void Update () {

    }
}
