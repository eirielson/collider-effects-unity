using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

	public List<AnimationClip> animations;
	public Transform m_transform;
	public float moveSpeed = 2.0f;
	public float runSpeed = 5.0f;		
	public float fowardSpeed = 1.0f;
	public float rotateSpeed = 10.0f;
	public CharacterController characterController;
	public float zVelocity = 0.0f;
	public float xVelocity = 0.0f;
	public float yVelocity = 0.0f;

	public bool isRunning = false;
	public bool isAttacking = false;
	public bool isHit = false;

	public AnimationClip walk;
	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip attack;
	public AnimationClip hit;
	public AnimationClip die;

	public GameObject checkHit;

	Collider m_collider;
	// Use this for initialization
	void Awake () {
		m_transform = transform;
		gameObject.AddComponent<CharacterController>();
		//gameObject.AddComponent<Rigidbody>();
		characterController = GetComponent<CharacterController> ();
		characterController.center = new Vector3(0, .65f, 0);
		characterController.radius = 0.3f;
		characterController.height = 1.3f;
		m_collider = GetComponent<Collider> ();
		ChangeAnimation ();
	}
	void SetAnimations(){
		walk.wrapMode = WrapMode.Loop;
		run.wrapMode = WrapMode.Loop;
		idle.wrapMode = WrapMode.Loop;
		attack.wrapMode = WrapMode.Once;		
		hit.wrapMode = WrapMode.Once;
		die.wrapMode = WrapMode.Once;
	}
	//Move o personagem
	public virtual void Move(){
		if (characterController.isGrounded)
			transform.Rotate (0, xVelocity, 0);
		Vector3 foward = transform.TransformDirection (Vector3.forward);
		characterController.SimpleMove (foward * zVelocity);
	}
	public void Attack(){

		if(!isAttacking){
			isAttacking = true;
			animation[attack.name].time = 0;
			animation.Blend(attack.name);
			CheckHit();
			StartCoroutine(stopAttack());
		}
	}
	public IEnumerator stopAttack(){
		yield return new WaitForSeconds(attack.length);
		isAttacking = false;
	}
	public void CheckHit(){

		StartCoroutine(ShootTrigger());

	}
	public IEnumerator ShootTrigger(){
		yield return new WaitForSeconds(attack.length/2);
		//instancia o objeto de tiro
		GameObject go_checkHit = (GameObject)  GameObject.Instantiate (checkHit, transform.position, transform.rotation);
		//seta o dano que o projetil causa ao colidir
		go_checkHit.GetComponent<CheckHit> ().Config (this, 5);
		//aplica a força na direção de disparo
		go_checkHit.rigidbody.velocity =  transform.forward * 100;
	}
	public void Hit(){
		isHit = true;
	}
	//Gerencia as animacoes 
	public virtual void ChangeAnimation(){
		if(characterController.isGrounded){
			if(characterController.isGrounded && !isAttacking){
				if(zVelocity==0 && xVelocity ==0){
					animation.CrossFade(idle.name, .25f);
				}else if(zVelocity > 0 || xVelocity != 0){
					animation[walk.name].speed = 1;
					animation[run.name].speed = 1;
					if(!isRunning)
						animation.CrossFade(walk.name, .25f);
					else {
						animation.CrossFade(run.name, .25f);
					}
				} else if(zVelocity <0){
					animation[walk.name].speed = -1;
					animation[run.name].speed = -1;
					if(!isRunning)
						animation.CrossFade(walk.name, .25f);
					else {
						animation.CrossFade(run.name, .25f);
					}
				}
			}
			if(isAttacking){

					
			}
			if(isHit){
				isHit = false;
				animation.Play(hit.name);
			}
		}		
	}

}
