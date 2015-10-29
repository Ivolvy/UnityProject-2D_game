﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JewelCounter : MonoBehaviour {

    Text textJewels;
    float nbOfJewels = 0;

    public AudioClip collect;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addJewelCount() {
        nbOfJewels += 1;
        audioSource.PlayOneShot(collect);
        GameObject.Find("nbOfJewels").GetComponent<Text>().text = nbOfJewels.ToString() + "x";
    }

}
