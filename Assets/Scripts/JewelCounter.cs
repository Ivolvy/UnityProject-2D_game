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

    GameObject GoPlayer;
    GameObject GoCrate;
    GameObject GoBlade;
    GameObject MainCamera;

    public AudioClip partyMusic;


    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        lighting_Sprite = Resources.Load("Lighting_Sprite", typeof(Material)) as Material;
        GoPlayer = GameObject.Find("Player");
        GoCrate = GameObject.Find("crate");
        GoBlade = GameObject.Find("blade");
        MainCamera = GameObject.Find("MainCamera");
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

            modifyGameSong();
            modifyLightingOnSprites();

            party.SetActive(true);
            discoBall.SetActive(true);
        }
    }


    void modifyGameSong() {
        MainCamera.GetComponent<AudioSource>().clip = partyMusic;
        MainCamera.GetComponent<AudioSource>().Play();
    }

    //modifiy material in order to illuminate the objects
    void modifyLightingOnSprites() {
        GoPlayer.GetComponent<Renderer>().material = lighting_Sprite;
        GoCrate.GetComponent<Renderer>().material = lighting_Sprite;
        GoBlade.GetComponent<Renderer>().material = lighting_Sprite;
    }

}
