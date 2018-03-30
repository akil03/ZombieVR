using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour {

	//public Text fpsText;
	float fps;
	public Text scoreText;
	public int count= 0;
	public Text highscoreText;
	public int highscore;
	public Text gameoverText;
	public GameObject[] Button;
	public Image bloodSplat;
	public List<Text> WaveNoText;
	public Text surviveText;
	public Text winText;
	public bool isWave;
	public Image FillImage;
	bool isFade;
	public Image startImage;
	public GameObject maincam;
	public Slider startSlider;

	public static UIManager instance;
	// Use this for initialization
	void Awake(){
		UnityEngine.XR.XRSettings.enabled = false;
		startImage.gameObject.SetActive (true);
		//Time.timeScale = 0f;
		instance = this;
		isWave = false;
	}
	void Start () {
//		Screen.sleepTimeout = SleepTimeout.NeverSleep;
//		highscore = PlayerPrefs.GetInt ("Highscore");
//		highscoreText.text = "Legendary numbers: " +highscore.ToString ();
//		InvokeRepeating ("DisplayFPS", 0.1f, 0.5f);
//		StartCoroutine (DisplayWave (1));
		isFade = false;
		//PlayerPrefs.SetInt ("Highscore", 0);
	}
	
	// Update is called once per frame
	void Update () {

		if (startSlider.value == 0f) {
			NormalMode ();
		} else {
			VRMode ();
		}
		fps = 1.0f / Time.deltaTime;
		CalcScore ();
		DisplayScore ();

	}

	public void DisplayFPS(){
		//fpsText.text = " " + fps.ToString ();
	}

	public void CalcScore(){
		if (count > highscore) {
			highscore = count;
			PlayerPrefs.SetInt ("Highscore", highscore);
			if (!isFade) {
				highscoreText.DOFade (0, 0.8f).OnComplete (() => {
					highscoreText.DOFade (1, 0.8f).OnComplete (() => {
						highscoreText.DOFade (0, 0.8f).OnComplete (() => {
							highscoreText.DOFade (1, 0.8f).OnComplete (() => {
								isFade = true;
							});
						});
					});
				});
			}
		}
	}

	public void DisplayScore(){
		scoreText.text = "Slayed: " + count.ToString ();
		highscoreText.text = "Annihilated: " +highscore.ToString ();
	}

	public IEnumerator DisplayWave(int wavenum){
		yield return new WaitForSeconds (4f);
		AudioManager.instance.PlayWave ();
		WaveNoText [wavenum - 1].gameObject.SetActive (true);
		yield return new WaitForSeconds (2f);
		WaveNoText [wavenum - 1].DOFade (0, 3f).SetEase (Ease.Linear).OnComplete (() => {
			WaveNoText [wavenum - 1].gameObject.SetActive (false);
			WaveNoText [wavenum - 1].DOFade (1, 0.1f).SetEase (Ease.Linear);
			isWave = false;
			SpawnManager.instance.isSpawn = true;
		});
	}

	public void DisplaySurvive(){
		isWave = true;
		AudioManager.instance.PlaySurvive ();
		surviveText.gameObject.SetActive (true);
		surviveText.DOFade (0, 3f).SetEase (Ease.Linear).OnComplete (() => {
			surviveText.gameObject.SetActive(false);
			surviveText.DOFade(1,0.1f).SetEase(Ease.Linear);
		});
	}

	public IEnumerator DisplayWin(){
		isWave = true;
		SpawnManager.instance.isSpawn = false;
		SpawnManager.instance.isgameOver = true;
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Zombie");
		foreach (GameObject obj in objs) {
			DOTween.KillAll ();
			obj.SetActive (false);
		}
		yield return new WaitForSeconds (4f);
		AudioManager.instance.PlaySurvive ();
		winText.gameObject.SetActive (true);
		Button [2].SetActive (true);
	}

	public void ImageFill(){
		FillImage.gameObject.SetActive (true);
		FillImage.DOFillAmount (1, 3f).OnComplete (() => {
			FillImage.gameObject.SetActive (false);
			FillImage.DOFillAmount (0, 0.1f);
		});
	}

	public void ImageReverseFill(){
		DOTween.Kill (FillImage);
		FillImage.gameObject.SetActive (false);
		FillImage.DOFillAmount (0, 0.1f);
	}

	public void GoButton(){
		Time.timeScale = 1f;
		startImage.gameObject.SetActive (false);
	}

	public void NormalMode(){
		UnityEngine.XR.XRSettings.enabled = false;
		maincam.GetComponent<GyroController> ().enabled = true;
	}

	public void VRMode(){
		UnityEngine.XR.XRSettings.enabled = true;
		maincam.GetComponent<GyroController> ().enabled = false;
	}

	public void StartGame(){
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		SpawnManager.instance.isgameOver = false;
		highscore = PlayerPrefs.GetInt ("Highscore");
		highscoreText.text = "Legendary numbers: " +highscore.ToString ();
		scoreText.gameObject.SetActive (true);
		highscoreText.gameObject.SetActive (true);
		//InvokeRepeating ("DisplayFPS", 0.1f, 0.5f);
		Button [0].SetActive (false);
		Button [1].SetActive (false);
		SpawnManager.instance.CanvasRotate ();
		StartCoroutine (DisplayWave (1));
		StartCoroutine (AudioManager.instance.PlaySound ());
		SpawnManager.instance.isSpawn = true;
		SpawnManager.instance.oldIndex = SpawnManager.instance.index;
		SpawnManager.instance.waitTime = 9;
		SpawnManager.instance.StartSpawn ();
		//SpawnManager.instance.StartCoroutine(SpawnManager.instance.SpawnZombie());
	}

	public void Replay(){
		SceneManager.LoadScene ("test_1");
	}

	public void RunAway(){
		Application.Quit ();
		print ("quit");
	}
}
