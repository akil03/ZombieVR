using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ZombieBehaviour : MonoBehaviour {

	public float moveTime;
	public float ZombieHealth;
	public Animator _animator;
	public GameObject child;
	bool isWalk;
	bool isAttack;
	bool isHurt;
	bool isDead;
	public AudioSource _audioSource;
	public AudioClip moveSound,AttackSound,playerDeathsound,selectedSound;

	void Awake(){
		switch (SpawnManager.instance.waveNo) {
		case 1:
			ZombieHealth = 50f;
			break;
		case 2:
			ZombieHealth = 100f;
			break;
		case 3:
			ZombieHealth = 150f;
			break;
		case 4:
			ZombieHealth = 200f;
			break;
		case 5:
			ZombieHealth = 250f;
			break;
		default:
			break;

		}
	}

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
		isDead = false;
		selectedSound = moveSound;
		//moveTime = 22f - (SpawnManager.instance.waveNo * 2);
		moveTime = 17f;
		transform.DOMove (Camera.main.transform.position, moveTime, false).SetEase(Ease.Linear).OnComplete (() => {
			isAttack = true;
			Attack();
			//CameraShake2.instance.StartShake(SpawnManager.instance.prop);
			isWalk = false;
			isHurt = true;
			selectedSound = AttackSound;
			//InvokeRepeating("ShakeCam", 0.01f, 0.8f);
			if(!isDead){
				Invoke("GameOver", 4.2f);
				//Invoke("ReloadLevel", 30f);
			}
		});

	}

	public void AdjustHealth(float health){
		
		ZombieHealth = ZombieHealth - health;
		if (ZombieHealth <= 0f) {
			CancelInvoke ("GameOver");
			_animator.Play ("Death");
			isDead = true;
			isHurt = false;
			CancelInvoke ();
			UIManager.instance.bloodSplat.gameObject.SetActive(false);
			UIManager.instance.count++;
			child.transform.SetParent (null);
			if (!CombatControl.instance.isHeadShot) {
				CombatControl.instance.DestroyEnemy ();
			} else {
				Destroy (transform.gameObject);

			}
			Destroy (child, 2f);
			//CameraShake.instance.shakeDuration = 0f;
		}
	}

	public void ReloadLevel(){
		SceneManager.LoadScene ("test_1");
	}

	public void GameOver(){
		SpawnManager.instance.CanvasRotate ();
		UIManager.instance.gameoverText.gameObject.SetActive (true);
		UIManager.instance.Button[2].SetActive (true);
		UIManager.instance.isWave = true;
		selectedSound = null;
		SpawnManager.instance.isSpawn = false;
		SpawnManager.instance.Death ();
		CancelInvoke ("Attack");
		AudioManager.instance.StopAllCoroutines ();
		SpawnManager.instance.isgameOver = true;
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Zombie");
		foreach (GameObject obj in objs) {
			DOTween.KillAll ();
			obj.SetActive (false);
		}
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
		if (!isDead) {
			Invoke ("LateAtkDisplay", 0.3f);
			_animator.SetInteger ("Attackparam", 1);
			_animator.Play ("Attack");
			Invoke ("Breather", 1.5f);
			Invoke ("Attack", 2f);
		}
	}

	public void LateAtkDisplay(){
		if (!isDead) {
			UIManager.instance.bloodSplat.gameObject.SetActive (true);
			UIManager.instance.bloodSplat.DOFade (0, 0.5f).SetEase (Ease.Linear).OnComplete (() => {
				UIManager.instance.bloodSplat.gameObject.SetActive (false);
				UIManager.instance.bloodSplat.DOFade (1, 0.01f);
			});
		}
	}

	public void Breather(){
		_animator.SetInteger ("Attackparam", 0);
	}

//	public void ShakeCam(){
//		CameraShake.instance.shakeDuration = 10f;
//		CameraShake.instance.Shake();
//	}
}
