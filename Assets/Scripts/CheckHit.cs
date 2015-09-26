using UnityEngine;
using System.Collections;

public class CheckHit : MonoBehaviour {

	Character m_character;
	Vector3 m_initialPos;
	float m_distance;

	public void Config(Character character, float distance ){
		m_character = character;
		m_initialPos = transform.position;
		m_distance = distance;
	}
	void OnTriggerEnter(Collider other) {

		if(other.gameObject != m_character.m_transform.gameObject)
			if(other.GetComponent<Character>())
			{
				Character character = other.GetComponent<Character>();
				character.Hit ();
			}
	}
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(m_initialPos, transform.position) > m_distance)
			Destroy(this.gameObject);

	}
}
