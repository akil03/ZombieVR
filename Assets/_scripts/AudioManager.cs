using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public List<AudioClip> BGSound;
	public AudioClip tempClip;
	public AudioClip waveClip;
	public AudioClip surviveClip;

	public static AudioManager instance;
//	public List<AudioClip> ReserveSound;

	void Awake(){
		instance = this;
	}

	void Start () {
		
		//StartCoroutine(PlaySound());
	}

	void Update () {




	}

//	public void BGNoises(){
//		int index = Random.Range (0, BGSound.Count);
//		GetComponent<AudioSource> ().PlayOneShot (BGSound [index]);
//		//BGSound.RemoveAt [index];
//	}


//	IEnumerator PlaySound(){
//		do {
//			index = Random.Range (0, BGSound.Count);
//			GetComponent<AudioSource> ().PlayOneShot (BGSound[index],0.5f);
//			yield return new WaitForSeconds (BGSound[index].length + 3f);
//			ReserveSound.Add (BGSound [index]);
//			BGSound.RemoveAt (index);
//		} while(BGSound.Count >= 1);
//
//		while(ReserveSound.Count >=2){
//			int i = Random.Range (0, ReserveSound.Count);
//			BGSound.Add (ReserveSound [i]);
//			ReserveSound.RemoveAt (i);
//		}
//		StartCoroutine (PlaySound ());
//	}

	int index;
	public IEnumerator PlaySound(){
		yield return new WaitForSeconds (10f);
		for (int i = 0; i <= 10000; i++) {
			if(!UIManager.instance.isWave){
				SoundPlay ();
				yield return new WaitForSeconds (tempClip.length + 6f);
			}else
				yield return new WaitForSeconds (2f);
		}
	}

	public void SoundPlay(){
		index = Random.Range (0, BGSound.Count);
		GetComponent<AudioSource> ().PlayOneShot (BGSound[index]);
		while (tempClip != null) {
			BGSound.Add (tempClip);
			tempClip = null;
		}
		tempClip = BGSound [index];
		BGSound.RemoveAt (index);
	}

	public void PlayWave(){
		GetComponent<AudioSource> ().PlayOneShot (waveClip);
	}

	public void PlaySurvive(){
		GetComponent<AudioSource> ().PlayOneShot (surviveClip);
	}
}
