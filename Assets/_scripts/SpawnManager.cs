using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	public Transform[] spawnPoint;
	public GameObject[] zombiePrefab;
	public GameObject selectedPrefab;
	public GameObject Clone;
	public bool isSpawn;
	public int index, oldIndex;
	int oldzombieIndex;
	int zombieIndex;
	public CameraShake2.Properties prop;
	AudioSource _audiosource;
	public AudioClip deathClip;
	public float waitTime = 8f;
	public float startWait;
	public int waveNo;
	public float WaveDuration;
	public float Timer;
	public bool isgameOver;
	public GameObject CameraCon;
	public GameObject CanvasCon;
	Vector3 newRotation;

	public static SpawnManager instance;
	// Use this for initialization
	void Awake(){
		_audiosource = GetComponent<AudioSource> ();
		instance = this;
		waveNo = 1;
		WaveDuration = 90f;
	}
	void Start () {
		isgameOver = true;
		oldzombieIndex = zombieIndex;
//		isSpawn = true;
//		oldIndex = index;
//		waitTime = 9;
//		//InvokeRepeating ("SpawnzZombie", 0.5f, 8f);
//		StartCoroutine(SpawnZombie());
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
					isSpawn = false;
					ClearZombies ();
					//CanvasCon.gameObject.transform.localRotation = CameraCon.gameObject.transform.localRotation;
					CanvasRotate();
					UIManager.instance.DisplaySurvive ();
					UIManager.instance.StartCoroutine (UIManager.instance.DisplayWave (waveNo));
					waitTime = 8;
					StartCoroutine (SpawnZombie ());
					break;
				case 3:
					StopCoroutine (SpawnZombie ());
					isSpawn = false;
					ClearZombies ();
					CanvasRotate();
					UIManager.instance.DisplaySurvive ();
					UIManager.instance.StartCoroutine (UIManager.instance.DisplayWave (waveNo));
					waitTime = 7;
					StartCoroutine (SpawnZombie ());
					break;
				case 4:
					StopCoroutine (SpawnZombie ());
					isSpawn = false;
					ClearZombies ();
					CanvasRotate();
					UIManager.instance.DisplaySurvive ();
					UIManager.instance.StartCoroutine (UIManager.instance.DisplayWave (waveNo));
					waitTime = 6;
					StartCoroutine (SpawnZombie ());
					break;
				case 5:
					StopCoroutine (SpawnZombie ());
					isSpawn = false;
					ClearZombies ();
					CanvasRotate();
					UIManager.instance.DisplaySurvive ();
					UIManager.instance.StartCoroutine (UIManager.instance.DisplayWave (waveNo));
					waitTime = 5;
					StartCoroutine (SpawnZombie ());
					break;
				default:
					StopCoroutine (SpawnZombie ());
					isSpawn = false;
					ClearZombies ();
					newRotation = new Vector3 (Mathf.Clamp (CameraCon.transform.eulerAngles.x, 0, 1), CameraCon.transform.eulerAngles.y, CameraCon.transform.eulerAngles.z);
					CanvasCon.transform.eulerAngles = newRotation;
					UIManager.instance.DisplaySurvive ();
					UIManager.instance.StartCoroutine (UIManager.instance.DisplayWin ());
					break;

				}
				Timer = 0f;
				WaveDuration = Time.deltaTime + 90f;
			}
		}
	}

	public void SpawnzZombie(){
		index = Random.Range (0, spawnPoint.Length);
		while (index == oldIndex) {
			index = Random.Range (0, spawnPoint.Length);
		}
		
		Clone = Instantiate (selectedPrefab, spawnPoint [index]);
		Clone.gameObject.transform.localPosition = spawnPoint [index].transform.position;
		Clone.gameObject.transform.localRotation = Quaternion.identity;
		Clone.transform.SetParent (null);

		oldIndex = index;
	}

	public void Death(){
		_audiosource.PlayOneShot (deathClip, 1f);
	}

	public IEnumerator SpawnZombie(){
		yield return new WaitForSeconds (startWait);
//		for (int i = 0; i <= 10000; i++) {
//			SpawnzZombie ();
//			yield return new WaitForSeconds (waitTime);
//		}
		while(isSpawn){
			SelectZombie ();
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

	public void CanvasRotate(){
		newRotation = new Vector3(Mathf.Clamp(CameraCon.transform.eulerAngles.x, 0, 1), CameraCon.transform.eulerAngles.y, CameraCon.transform.eulerAngles.z);
		CanvasCon.transform.eulerAngles = newRotation;
	}

	public void StartSpawn(){
		StartCoroutine (SpawnZombie ());
	}

	public void SelectZombie(){
		zombieIndex = Random.Range (0, zombiePrefab.Length);
		while (zombieIndex == oldzombieIndex) {
			zombieIndex = Random.Range (0, zombiePrefab.Length);
		}
		selectedPrefab = zombiePrefab [zombieIndex];
		oldzombieIndex = zombieIndex;
	}
}
