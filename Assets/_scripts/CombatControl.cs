using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatControl : MonoBehaviour {

	public bool isHit;
	RaycastHit hit;

	public static CombatControl instance;

	void Awake(){
		instance = this;	
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GunRay ();
	}

	public void GunRay(){
		
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/4, Screen.height/2, 0));
		if(Physics.Raycast(ray, out hit,10f)){
			//Debug.Log ("Collision");
			if (hit.collider.tag == "Zombie") {
				//isHit = true;
				//Destroy (hit.transform.gameObject,1f);
				ZombieBehaviour ZB =  hit.collider.gameObject.GetComponent<ZombieBehaviour> ();
				ZB.AdjustHealth (33f);					
				//print (hit.collider.name);					
			}
		}
	}

	public void DestroyEnemy(){
		Destroy (hit.transform.gameObject);
	}
}
