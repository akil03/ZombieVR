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
	public float waitTime = 8f;
	public float startWait;
	public int waveNo;
	public float WaveDuration;
	public float Timer;
	public bool isgameOver;

	public static SpawnManager instance;
	// Use this for initialization
	void Awake(){
		_audiosource = GetComponent<AudioSource> ();
		instance = this;
		waveNo = 1;
		WaveDuration = 57f;
		isgameOver = false;
	}
	void Start () {
		isSpawn = true;
		oldIndex = index;
		waitTime = 9;
		//InvokeRepeating ("SpawnzZombie", 0.5f, 8f);
		StartCoroutine(SpawnZombie());

	}
	
	// Update is called once per frame
	void Update () {
		if (!isgameOver) {
			Timer += Time.deltaTime;
			if (Timer >= WaveDuration) {
				waveNo++;
				switch (waveNo) {
				case 1:
					break;
				case 2:
					StopCoroutine (SpawnZombie ());
					ClearZombies ();
					UIManager.instance.DisplaySurvive ();
					UIManager.instance.StartCoroutine (UIManager.instance.DisplayWave (waveNo));
					waitTime = 8;
					StartCoroutine (SpawnZombie ());
					break;
				case 3:
					StopCoroutine (SpawnZombie ());
					ClearZombies ();
					UIManager.instance.DisplaySurvive ();
					UIManager.instance.StartCoroutine (UIManager.instance.DisplayWave (waveNo));
					waitTime = 8;
					StartCoroutine (SpawnZombie ());
					break;
				case 4:
					StopCoroutine (SpawnZombie ());
					ClearZombies ();
					UIManager.instance.DisplaySurvive ();
					UIManager.instance.StartCoroutine (UIManager.instance.DisplayWave (waveNo));
					waitTime = 8;
					StartCoroutine (SpawnZombie ());
					break;
				case 5:
					StopCoroutine (SpawnZombie ());
					ClearZombies ();
					UIManager.instance.DisplaySurvive ();
					UIManager.instance.StartCoroutine (UIManager.instance.DisplayWave (waveNo));
					waitTime = 8;
					StartCoroutine (SpawnZombie ());
					break;
				default:
					break;

				}
				Timer = 0f;
				WaveDuration = Time.deltaTime + 57f;
			}
		}
	}

	public void SpawnzZombie(){
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

	public IEnumerator SpawnZombie(){
		yield return new WaitForSeconds (startWait);
		for (int i = 0; i <= 1000; i++) {
			SpawnzZombie ();
			yield return new WaitForSeconds (waitTime);
		}
	}

	public void ClearZombies(){
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Zombie");
		foreach (GameObject obj in objs) {
			Destroy (obj);
		}
	}
}
