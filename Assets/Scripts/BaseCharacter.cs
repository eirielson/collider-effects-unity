﻿using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour {

	/// Enum para o state machine do character
	public enum State{
		idle,
		walk,
		run,
		attack,
		hit,
		die
	}
	
	public AnimationClip idle;
	public AnimationClip walk;
	public AnimationClip run;
	public AnimationClip attack;
	public AnimationClip hit;
	public AnimationClip die;


	public State actualState;
	public State nextState;
	public State previousState;


	#region characterVars
	float speed = 1.0f;
	protected float verticalSpeed = 0.0f; //total velocidade em x
	protected float horizontalSpeed = 0.0f; // velocidade em Y

	public float walkSpeed= 2.0f;
	public float runSpeed = 5.0f;
	protected bool isHit;
	protected bool isDead;
	protected bool isRunning;		
	CharacterController m_characterController;
	#endregion characterVars

	/// <summary>
	/// Um passo anterior ao Start. Prepara o objeto para estar pronto no método start
	/// </summary>
	void Awake(){
		m_characterController = gameObject.AddComponent<CharacterController> ();

		m_characterController.center = new Vector3 (0.0f, 0.62f, 0.0f);
		m_characterController.radius = 0.35f;
		m_characterController.height = 1.21f;

		attack.wrapMode = WrapMode.Once;
		hit.wrapMode = WrapMode.Once;
	}


	// Use this for initialization
	// x 0.02  0.6,0 
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual	void Move(){
		if (!m_characterController.isGrounded) { //se o personagem está sobre um collider.
			transform.Rotate(0,horizontalSpeed,0);
			Vector3 forward = transform.TransformDirection(Vector3.forward); // obtem a direção do transform. Obtem o Z dele (para frente)


			float actualSpeed = actualState == State.run ? runSpeed : walkSpeed;
			m_characterController.SimpleMove(forward * verticalSpeed);
		}
	}

	public void SetState(State state){
		previousState = actualState;
		actualState = state;
		SetAnimationState ();
	}


	public void SetAnimationState(){
		if (actualState != State.hit && 
		    actualState != State.die && 
		    actualState != State.attack) {

			if(actualState == State.walk){
				animation.CrossFade(walk.name,0.25f);  // faz a transição de uma animação para outra em determinado tempo.
			}
			else if (actualState == State.run) {
				animation.CrossFade(run.name, 0.25f);
			}
			else if (actualState == State.idle) {
				animation.CrossFade(idle.name, 0.25f);
			}
		}

	}

	private void Attack(){
		if (actualState != State.attack) {
			previousState = actualState;
			actualState = State.attack;
			animation [attack.name].time = 0;
			animation.Blend (attack.name, 1.0f);
			StartCoroutine (CancelAttack ());
		}
	}

	private void Hit(){
		previousState = actualState;
		actualState = State.hit;
		animation[hit.name].time = 0;
		animation.CrossFade (hit.name, 0.25f);
		StartCoroutine (CancelHit ());
	}

	private IEnumerator CancelAttack(){
		yield return new WaitForSeconds (attack.length);
		SetState(previousState);
	}
	private IEnumerator CancelHit(){
		yield return new WaitForSeconds (hit.length);
		SetState(previousState);
	}

	public void OnGUI(){
		if (GUILayout.Button ("State Walk")) {
			SetState(State.walk);
		}
		if (GUILayout.Button ("State Run")) {
			SetState(State.run);
		}
		if (GUILayout.Button ("State Idle")) {
			SetState(State.idle);
		}
		if (GUILayout.Button ("State Attack")) {
			Attack();
		}
		if (GUILayout.Button ("State Hit")) {
			Hit ();
		}
		/*speed = GUILayout.HorizontalSlider (speed, -10.0f, 10.0f);

		foreach (AnimationState a in animation) {
			if(GUILayout.Button(a.name)){
				animation.Play(a.name);
			}
			a.speed = speed;
		}
		*/

	}

}
