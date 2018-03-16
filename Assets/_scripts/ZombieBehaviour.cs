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


	void Start () {
		gameObject.transform.LookAt (Camera.main.transform);
		//CameraShake2.instance.StartShake(SpawnManager.instance.prop);
		Movement ();
		//moveSpeed = Random.Range (0.1f, 1.3f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Movement(){
		ZombieNoises.instance.isWalk = true;
		transform.DOMove (Camera.main.transform.position, moveTime, false).SetEase(Ease.Linear).OnComplete (() => {
			_animator.Play ("Attack");
			CameraShake2.instance.StartShake(SpawnManager.instance.prop);
			ZombieNoises.instance.isWalk = false;
			ZombieNoises.instance.isAttack = true;
			//InvokeRepeating("ShakeCam", 0.01f, 0.8f);
			Invoke("ReloadLevel", 10f);
		});
	}

	public void AdjustHealth(float health){
		ZombieHealth = ZombieHealth - health;
		if (ZombieHealth <= 0f) {
			_animator.Play ("Death");
			//ZombieNoises.instance.isAttack = false;
			ZombieNoises.instance.StopAllCoroutines ();
			ZombieNoises.instance.isAudio = false;
			CancelInvoke ();
			child.transform.SetParent (null);
			CombatControl.instance.DestroyEnemy ();
			Destroy (child, 2f);
			//CameraShake.instance.shakeDuration = 0f;
		}
	}

	public void ReloadLevel(){
		SceneManager.LoadScene ("test_1");
	}

//	public void ShakeCam(){
//		CameraShake.instance.shakeDuration = 10f;
//		CameraShake.instance.Shake();
//	}
}
