using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JewelCounter : MonoBehaviour {

    Text textJewels;
    float nbOfJewels = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addJewelCount() {
        nbOfJewels += 1;
        GameObject.Find("nbOfJewels").GetComponent<Text>().text = nbOfJewels.ToString() + "x";
    }

}
