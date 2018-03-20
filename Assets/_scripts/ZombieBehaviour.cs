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
	bool isWalk;
	bool isAttack;
	bool isHurt;
	public AudioSource _audioSource;
	public AudioClip moveSound,AttackSound,playerDeathsound,selectedSound;

	void Start () {
		_audioSource = GetComponentInChildren<AudioSource> ();

		gameObject.transform.LookAt (Camera.main.transform);
		//CameraShake2.instance.StartShake(SpawnManager.instance.prop);
		Invoke("PlaySound",3);
		Movement ();
		Destroy (gameObject, 40f);

		//moveSpeed = Random.Range (0.1f, 1.3f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Movement(){
		isWalk = true;
		isAttack = false;
		selectedSound = moveSound;
		transform.DOMove (Camera.main.transform.position, moveTime, false).SetEase(Ease.Linear).OnComplete (() => {
			Attack();
			//CameraShake2.instance.StartShake(SpawnManager.instance.prop);
			isWalk = false;
			isAttack = true;
			isHurt = true;
			selectedSound = AttackSound;
			//InvokeRepeating("ShakeCam", 0.01f, 0.8f);
			Invoke("GameOver", 4.2f);
			Invoke("ReloadLevel", 9f);
		});
	}

	public void AdjustHealth(float health){
		
		ZombieHealth = ZombieHealth - health;
		if (ZombieHealth <= 0f) {
			CancelInvoke ("GameOver");
			_animator.Play ("Death");
			isHurt = false;
			CancelInvoke ();
			UIManager.instance.bloodSplat.gameObject.SetActive(false);
			UIManager.instance.count++;
			child.transform.SetParent (null);
			CombatControl.instance.DestroyEnemy ();
			Destroy (child, 2f);
			//CameraShake.instance.shakeDuration = 0f;
		}
	}

	public void ReloadLevel(){
		SceneManager.LoadScene ("test_1");
	}

	public void GameOver(){
		UIManager.instance.gameoverText.gameObject.SetActive (true);
		selectedSound = null;
		SpawnManager.instance.isSpawn = false;
		SpawnManager.instance.Death ();
		CancelInvoke ("Attack");
		AudioManager.instance.StopAllCoroutines ();
		Destroy (UIManager.instance.bloodSplat);
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Zombie");
		foreach (GameObject obj in objs)
			obj.SetActive (false);
	}
		
	public void PlaySound(){
		_audioSource.clip = selectedSound;
		_audioSource.Play ();
		if (isWalk)
			_audioSource.loop = true;
		if (isAttack)
			_audioSource.loop = false;
			Invoke("PlaySound",1f);
	}

	public void Attack(){
		_animator.SetInteger ("Attackparam", 1);
		_animator.Play ("Attack");
		Invoke ("Breather", 1.5f);
		Invoke ("LateAtkDisplay", 0.3f);
		Invoke("Attack", 2f);
	}

	public void LateAtkDisplay(){
		UIManager.instance.bloodSplat.gameObject.SetActive (true);
		UIManager.instance.bloodSplat.DOFade (0, 1f).SetEase (Ease.Linear).OnComplete (() => {
			UIManager.instance.bloodSplat.gameObject.SetActive (false);
			UIManager.instance.bloodSplat.DOFade (1, 0.1f);
		});
	}

	public void Breather(){
		_animator.SetInteger ("Attackparam", 0);
	}
//	public void BloodSplat(){
//		if(isHurt){
//			UIManager.instance.bloodSplat.gameObject.SetActive(true);
//			UIManager.instance.bloodSplat.DOFade(0.1, 5f).OnComplete(()=>{
//				UIManager.instance.bloodSplat.gameObject.SetActive(false);
//				UIManager.instance.bloodSplat.gameObject.SetActive(true);
//			});
//		}
//	}

//	public void ShakeCam(){
//		CameraShake.instance.shakeDuration = 10f;
//		CameraShake.instance.Shake();
//	}
}
