using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	public Transform[] spawnPoint;
	public GameObject zombiePrefab;
	public GameObject Clone;
	public bool isSpawn;
	int index, oldIndex;
	public CameraShake2.Properties prop;
	public AudioClip deathClip;
	AudioSource _audiosource;

	public static SpawnManager instance;
	// Use this for initialization
	void Awake(){
		_audiosource = GetComponent<AudioSource> ();
		instance = this;
	}
	void Start () {
		isSpawn = true;
		InvokeRepeating ("SpawnZombie", 0.5f, 8f);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnZombie(){
		if (isSpawn) {
			index = Random.Range (0, spawnPoint.Length);
			while (index == oldIndex) {
				index = Random.Range (0, spawnPoint.Length);
			}
			
			Clone = Instantiate (zombiePrefab, spawnPoint [index]);
			Clone.gameObject.transform.localPosition = spawnPoint [index].transform.position;
			Clone.gameObject.transform.localRotation = Quaternion.identity;
			Clone.transform.SetParent (null);

			oldIndex = index;
			isSpawn = true;
		}
	}

	public void Death(){
		_audiosource.PlayOneShot (deathClip, 1f);
	}
}
