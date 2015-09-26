using UnityEngine;
using System.Collections;

public class PlayerCharacter : Character {
	public bool ray = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(ray){
			Debug.DrawRay(transform.position, transform.forward);
			RaycastHit hit;
			float range = 5.0f;
			
			if (Physics.Raycast(transform.position, transform.forward, out hit, range)) {
				range = hit.distance;
				if(hit.transform.GetComponent<Character>()){
					hit.transform.GetComponent<Character>().Hit();
				}
				Debug.Log(hit.transform.name);
			}
		}

		GetIput ();	

		ChangeAnimation ();
	}

	void GetIput(){

		isRunning = Input.GetButton ("Run") == true ? true : false;
		if(!isAttacking){
			if(!isRunning)
				zVelocity = Input.GetAxis("Vertical") * fowardSpeed * moveSpeed;
			else {
				zVelocity = Input.GetAxis("Vertical") * fowardSpeed * runSpeed;
			}
			xVelocity = Input.GetAxis("Horizontal") * rotateSpeed;
		}

		if(Input.GetButtonDown("Fire1"))
			Attack();
		Move ();
	}
}
 