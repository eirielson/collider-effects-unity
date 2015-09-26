using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public Transform teleportTo;

	public void OnTriggerEnter(Collider other){
		Debug.Log (other.gameObject.name);
		other.transform.position = teleportTo.position;
	}

}