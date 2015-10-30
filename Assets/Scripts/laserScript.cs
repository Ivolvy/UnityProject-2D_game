using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class laserScript : MonoBehaviour {
	public Transform startPoint;
	//public Transform endPoint;
    public float timeInterval = 0.2f;
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
        GoLasersEndPoints = GameObject.Find("LasersEndPoints");
        lasersArray = new Transform[GoLasersEndPoints.transform.childCount];

        SetLasersEnd();
    }

    void SetLasersEnd() {

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
