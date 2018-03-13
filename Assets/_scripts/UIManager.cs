using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text fpsText;
	float fps;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("DisplayFPS", 0.1f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		fps = 1.0f / Time.deltaTime;


	}

	public void DisplayFPS(){
		fpsText.text = " " + fps.ToString ();
	}
}
