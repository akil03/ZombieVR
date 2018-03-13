using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour {

	public float moveSpeed;
	// Use this for initialization
	void Start () {
		moveSpeed = Random.Range (0.1f, 1.3f);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt (Camera.main.transform);

		gameObject.transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
	}
		
}
