using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ZombieBehaviour : MonoBehaviour {

	public float moveTime;
	public float ZombieHealth = 99f;
	public Animator _animator;
	public GameObject child;

	// Use this for initialization
	void Start () {

		gameObject.transform.LookAt (Camera.main.transform);
		Movement ();
		//moveSpeed = Random.Range (0.1f, 1.3f);
	}
	
	// Update is called once per frame
	void Update () {
//		if (CombatControl.instance.isHit) {
//			
//			_animator.Play ("Death");
//			CombatControl.instance.isHit = false;
//		}
	}

	public void Movement(){
		transform.DOMove (Camera.main.transform.position, moveTime, false).SetEase(Ease.Linear).OnComplete (() => {
			_animator.Play ("Attack");
			Invoke("ReloadLevel", 7f);
		});
	}

	public void AdjustHealth(float health){
		ZombieHealth = ZombieHealth - health;
		if (ZombieHealth <= 0f) {
			_animator.Play ("Death");
			CancelInvoke ();
			child.transform.SetParent (null);
			CombatControl.instance.DestroyEnemy ();
			Destroy (child, 2f);
		}
	}

	public void ReloadLevel(){
		SceneManager.LoadScene ("test_1");
	}
}
