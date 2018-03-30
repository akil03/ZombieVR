using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMechanics : MonoBehaviour {

	public AudioClip flareShotSound;

	public static GunMechanics instance;
	// Use this for initialization
	void Awake(){
		instance = this;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Shoot(){
		GetComponent<Animation> ().Play ("Shoot");
		GetComponent<AudioSource>().PlayOneShot(flareShotSound);
	}
}
