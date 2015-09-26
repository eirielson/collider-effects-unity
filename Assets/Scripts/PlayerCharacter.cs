using UnityEngine;
using System.Collections;

public class PlayerCharacter : BaseCharacter {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetInput ();
	}

	void GetInput(){
		horizontalSpeed = Input.GetAxis ("Horizontal"); // Edit > Project Setting > Input
		verticalSpeed = Input.GetAxis ("Vertical");
		isRunning = Input.GetButton ("Run");
		Move ();
	}
}
