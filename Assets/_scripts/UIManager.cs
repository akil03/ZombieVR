using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour {

	public Text fpsText;
	float fps;
	public Text scoreText;
	public int count= 0;
	public Text highscoreText;
	public int highscore;
	public Text gameoverText;
	public Image bloodSplat;

	public static UIManager instance;
	// Use this for initialization
	void Awake(){
		instance = this;
	}
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		highscore = PlayerPrefs.GetInt ("Highscore");
		highscoreText.text = "Legendary numbers: " +highscore.ToString ();
		InvokeRepeating ("DisplayFPS", 0.1f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		fps = 1.0f / Time.deltaTime;
		CalcScore ();
		DisplayScore ();

	}

	public void DisplayFPS(){
		fpsText.text = " " + fps.ToString ();
	}

	public void CalcScore(){
		if (count > highscore) {
			highscore = count;
			PlayerPrefs.SetInt ("Highscore", highscore);
		}
	}

	public void DisplayScore(){
		scoreText.text = "Zombies Slayed: " + count.ToString ();
		highscoreText.text = "Legendary numbers: " +highscore.ToString ();
	}
}
