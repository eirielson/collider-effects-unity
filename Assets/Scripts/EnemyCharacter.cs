using UnityEngine;
using System.Collections;

public class EnemyCharacter : Character {

	private Character target;
	public float activeDistance = 10.0f;
	public float attackDistance = 5.0f;
	public bool isActive = true;

	public enum State{
		idle,
		chasing,
		patrolling,
		attacking,
		hit
	}
	public State actualState;
	public Vector3 waypoint;
	public float distance;
	// Use this for initialization
	void Start () {
		target =(Character) GameObject.FindWithTag("Player").GetComponent<Character>() as Character;
	}	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(this.transform.position, target.m_transform.position);
		if(isActive)
			SetState();
	}
	public override void ChangeAnimation(){
		if(actualState == State.attacking){
			Vector3 lookPos = target.m_transform.position - transform.position;
			lookPos.y = 0;
			Quaternion rotation = Quaternion.LookRotation(lookPos);
			//aplica a rotação
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1.0f);
			if(animation[attack.name].weight > 0 && animation[attack.name].enabled){
				animation.PlayQueued(idle.name);
			}else{
				animation.CrossFade(attack.name);
				CheckHit();
			}
		} else if(actualState == State.idle){
			animation.CrossFade(idle.name, 0.25f);
		} else if(actualState == State.chasing){
			if(animation[run.name].weight <= 0){
				animation.CrossFade(run.name, 0.25f);
			}
		} else if(actualState == State.hit){
			if(animation[hit.name].weight <= 0){
				animation.CrossFade(hit.name, 0.05f);
				StartCoroutine(resetHit(hit.length));
			} 
		}
	}
	public IEnumerator resetHit(float time){
		yield return new WaitForSeconds (time);
		isHit = false;
	}
	public void SetState(){
		if(isHit)
			actualState = State.hit;
		else if(distance > activeDistance)
			actualState = State.idle;
		else if(distance < activeDistance && distance > attackDistance){
			actualState = State.chasing;
			ChaseTarget();
		}else if(distance <= attackDistance){
			actualState = State.attacking;
		}
		ChangeAnimation();
	}
	public void ChaseTarget(){
		Vector3 lookPos = target.m_transform.position - transform.position;
		lookPos.y = 0;
		Quaternion rotation = Quaternion.LookRotation(lookPos);
		//aplica a rotação
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1.0f);
		zVelocity = 1 * fowardSpeed * runSpeed;
		Vector3 foward = transform.TransformDirection (Vector3.forward);
		this.characterController.SimpleMove(foward * zVelocity);
	}
	void Patrol(){
		//Debug.Log("i`m patrolling");
	}
	void SetWaypoint(){

	}
}
