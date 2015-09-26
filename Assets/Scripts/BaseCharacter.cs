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
		
	#endregion

	// Use this for initialization
	void Start () {
		walk.wrapMode = WrapMode.PingPong;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	float speed = 1.0f;
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
