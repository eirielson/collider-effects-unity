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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
