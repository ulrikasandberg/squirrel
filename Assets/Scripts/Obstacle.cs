using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	public int damagePoints = 1;

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			Player player = other.gameObject.GetComponent<Player>();
			player.ApplyDamage(damagePoints);
		}
	}
}
