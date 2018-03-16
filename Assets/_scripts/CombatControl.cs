using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatControl : MonoBehaviour {

	public bool isHit = false;
	public bool isSecondHit;
	public bool isThirdHit;
	RaycastHit hit;
	public GameObject impactPrefab;

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
		
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
		if(Physics.Raycast(ray, out hit,10f)){
			//Debug.Log ("Collision");
			if (hit.collider.tag == "Zombie") {
				if (!isHit) {
					isHit = true;
					//isSecondHit = false;
					ZombieBehaviour ZB =  hit.collider.gameObject.GetComponent<ZombieBehaviour> ();
					ZB.AdjustHealth (33f);
					GameObject particle = Instantiate(impactPrefab, hit.point, Quaternion.identity);
					Destroy(particle, 2f);
					StartCoroutine(ShootInterval());
				}


//				if (isHit && !isSecondHit) {
//					GunMechanics.instance.Shoot ();
//					isSecondHit = true;
//					isThirdHit = false;
//					ZombieBehaviour ZB =  hit.collider.gameObject.GetComponent<ZombieBehaviour> ();
//					ZB.AdjustHealth (33f);
//					Invoke ("HitInterval", 1f);
//				}
//				if (isHit && isSecondHit && !isThirdHit) {
//					GunMechanics.instance.Shoot ();
//					isThirdHit = true;
//					isHit = false;
//					ZombieBehaviour ZB =  hit.collider.gameObject.GetComponent<ZombieBehaviour> ();
//					ZB.AdjustHealth (33f);
//				}
//				Invoke ("HitInterval", 1f);
//				if (!isHit)
//					CancelInvoke ();
									
				//print (hit.collider.name);					
			}
		}
	}

	public void DestroyEnemy(){
		Destroy (hit.transform.gameObject);
		StopCoroutine (ShootInterval ());
	}

	public void HitInterval(){
		isHit = false;
	}

	public IEnumerator ShootInterval(){
		GunMechanics.instance.Shoot ();
		yield return new WaitForSeconds (1f);
		isHit = false;
	}
}
