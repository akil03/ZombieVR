using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public List<AudioClip> BGSound;
	public List<AudioClip> ReserveSound;

	void Start () {
		
		StartCoroutine(PlaySound());
	}

	void Update () {
		
	}

//	public void BGNoises(){
//		int index = Random.Range (0, BGSound.Count);
//		GetComponent<AudioSource> ().PlayOneShot (BGSound [index]);
//		//BGSound.RemoveAt [index];
//	}

	int index;
	IEnumerator PlaySound(){
		do {
			index = Random.Range (0, BGSound.Count);
			GetComponent<AudioSource> ().PlayOneShot (BGSound[index],0.5f);
			yield return new WaitForSeconds (BGSound[index].length + 3f);
			ReserveSound.Add (BGSound [index]);
			BGSound.RemoveAt (index);
		} while(BGSound.Count >= 1);

		while(ReserveSound.Count >=2){
			int i = Random.Range (0, ReserveSound.Count);
			BGSound.Add (ReserveSound [i]);
			ReserveSound.RemoveAt (i);
		}
		StartCoroutine (PlaySound ());
	}
}
