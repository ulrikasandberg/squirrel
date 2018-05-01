using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : MonoBehaviour {

	public ParticleSystem particleSystem;
	private bool taken = false;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player" && !taken) {
			taken = true;

			Instantiate(particleSystem, transform.position, transform.rotation);

			Destroy(this.gameObject);
		}
	}
}
