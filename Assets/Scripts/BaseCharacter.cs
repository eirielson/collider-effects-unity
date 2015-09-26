using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour {

	/// Enum para o state machine do character
	enum State{
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


	#region
	float speed = 1.0f;
	float verticalSpeed = 0.0f; //total velocidade em x
	float horizontalSpeed = 0.0f; // velocidade em Y

	public float walkSpeed= 2.0f;
	public float runSpeed = 5.0f;
	bool isHit;
	bool isDead;
		
	CharacterController m_characterController;

	#endregion
	/// <summary>
	/// Um passo anterior ao Start. Prepara o objeto para estar pronto no método start
	/// </summary>
	void Awake(){

	}


	// Use this for initialization
	void Start () {
		walk.wrapMode = WrapMode.PingPong;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetState(){

	}


	public void OnGUI(){
		speed = GUILayout.HorizontalSlider (speed, -10.0f, 10.0f);

		foreach (AnimationState a in animation) {
			if(GUILayout.Button(a.name)){
				animation.Play(a.name);
			}
			a.speed = speed;
		}
	}

}
