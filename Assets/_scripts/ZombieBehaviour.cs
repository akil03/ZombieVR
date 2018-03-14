using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ZombieBehaviour : MonoBehaviour {

	public float moveTime;
	public Animator _animator;
	// Use this for initialization
	void Start () {

		gameObject.transform.LookAt (Camera.main.transform);
		Movement ();
		//moveSpeed = Random.Range (0.1f, 1.3f);
	}
	
	// Update is called once per frame
	void Update () {
		

		//gameObject.transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
	}

	public void Movement(){
		transform.DOMove (Camera.main.transform.position, moveTime, false).SetEase(Ease.Linear).OnComplete (() => {
			_animator.Play ("Attack");
			Invoke("ReloadLevel", 7f);
		});
	}

	public void ReloadLevel(){
		SceneManager.LoadScene ("test_1");
	}
}
