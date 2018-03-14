using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	public Transform[] spawnPoint;
	public GameObject zombiePrefab;
	public GameObject Clone;
	public bool isSpawn;
	int index, oldIndex;
	// Use this for initialization
	void Start () {

		InvokeRepeating ("SpawnZombie", 0.5f, 5f);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnZombie(){
		index = Random.Range (0, spawnPoint.Length);
		while (index == oldIndex) {
			index = Random.Range (0, spawnPoint.Length);
		}
			
		Clone =  Instantiate (zombiePrefab, spawnPoint[index]);
		Clone.gameObject.transform.localPosition = spawnPoint [index].transform.position;
		Clone.gameObject.transform.localRotation = Quaternion.identity;
		Clone.transform.SetParent (null);

		oldIndex = index;

		isSpawn = true;
	}
}
