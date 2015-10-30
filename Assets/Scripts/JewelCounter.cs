using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JewelCounter : MonoBehaviour {

    Text textJewels;
    float nbOfJewels = 0;

    public AudioClip collect;
    AudioSource audioSource;

    public GameObject party;
    public GameObject discoBall;

    public Material lighting_Sprite;
    public GameObject GoPlayer;


    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        lighting_Sprite = Resources.Load("Lighting_Sprite", typeof(Material)) as Material;
        GoPlayer = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addJewelCount() {
        nbOfJewels += 1;
        audioSource.PlayOneShot(collect);
        GameObject.Find("nbOfJewels").GetComponent<Text>().text = nbOfJewels.ToString() + "x";
 
        //we enter party mode!
        if(nbOfJewels == 1) {
            GoPlayer.GetComponent<Renderer>().material = lighting_Sprite;
            party.SetActive(true);
            discoBall.SetActive(true);
        }
    }

}
