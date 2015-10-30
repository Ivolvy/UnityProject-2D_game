using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class laserScript : MonoBehaviour {
	public Transform startPoint;
	public Transform endPoint;
    public float timeInterval = 0.2f;
    public float laserWidth = 0.2f;
    public bool isRandom = false;

    LineRenderer laserLine;

    GameObject laser;

    int lastIndex = 0;
    int currentIndex = 0;

    Transform[] lasersArray;


    // Use this for initialization
    void Start () {
		laserLine = GetComponentInChildren<LineRenderer> ();
		laserLine.SetWidth (laserWidth, laserWidth);
        lasersArray = new Transform[transform.childCount];

        SetLasersEnd();
    }

    void SetLasersEnd() {
        foreach (Transform child in transform) {
            child.name = "LasersEnd-" + lastIndex;
            lasersArray[lastIndex] = child;
            lastIndex++;
        }

        StartCoroutine(TraceLasers());
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


    /*    foreach (Transform child in transform) {

            lasersArray[currentIndex] = child;

            laserLine.SetPosition(0, startPoint.position);
            laserLine.SetPosition(1, child.position);
            yield return new WaitForSeconds(timeInterval);
            currentIndex++;
            if (currentIndex == lastIndex) {
                currentIndex = 0;
                StartCoroutine(TraceLasers());
            }
        }*/
    }



    // Update is called once per frame
    void Update () {

    }
}
