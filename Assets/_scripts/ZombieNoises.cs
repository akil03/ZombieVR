using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNoises : MonoBehaviour {

	public bool isWalk;
	public bool isAttack;
	public bool isAudio;
	//public List<AudioClip> ZombieSounds;
	public AudioClip walkNoise;
	public AudioClip attackNoise;
	AudioSource aud;

	public static ZombieNoises instance;
	// Use this for initialization

	void Awake(){
		instance = this;
	}

	void Start () {
		aud = GetComponent<AudioSource> ();
		isAudio = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isAudio) {
			if (isWalk && !isAttack) {
				StartCoroutine (ZombieWalk ());
			}
		}
	}

	IEnumerator ZombieWalk(){
		while (true) {
			isAudio = true;
			if (isAttack) {
				StartCoroutine (ZombieAttack ());
			}
			//int index = Random.Range (0, ZombieSounds.Count);
			//aud.PlayOneShot (ZombieSounds [index]);
			aud.PlayOneShot(walkNoise);
			yield return new WaitForSeconds (3f);
		}
		StartCoroutine (ZombieWalk ());

	}

	IEnumerator ZombieAttack(){
		while (true) {
			isAudio = true;
			aud.PlayOneShot (attackNoise);
			yield return new WaitForSeconds (0.8f);
		}
		StartCoroutine (ZombieAttack ());
	}
}
