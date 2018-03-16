using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNoises : MonoBehaviour {

	public bool isMove;
	public bool isAttack;
	bool isAudio;
	public List<AudioClip> ZombieSounds;
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
			if (isMove && !isAttack) {
				StartCoroutine (ZombieSound ());
			}
		}
	}

	IEnumerator ZombieSound(){
		while (true) {
			isAudio = true;
			int index = Random.Range (0, ZombieSounds.Count);
			aud.PlayOneShot (ZombieSounds [index]);
			yield return new WaitForSeconds (3f);
		}
		StartCoroutine (ZombieSound ());
	}
}
