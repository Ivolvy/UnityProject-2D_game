using UnityEngine;
using System.Collections;


public class JewelCollider : MonoBehaviour {

   

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        GameObject Jewel = GameObject.Find("JewelCounter");
        JewelCounter jewelCounter = (JewelCounter)Jewel.GetComponent(typeof(JewelCounter));

        GameObject Go = this.gameObject;
        jewelCounter.addJewelCount();
        Destroy(Go);
    }
}
