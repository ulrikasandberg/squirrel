using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpider : MonoBehaviour {

	public float downSpeed = 10f;
	public float upSpeed = 3f;
	public int damagePoints = 1;

	private bool playerIsComming;
	private bool touchedGroundOrPlayer;
	private Vector3 startPosition;
	private enum States {IDLE, ATTACK, RETREAT};
	private States state;

	// Use this for initialization
	void Start () {
		playerIsComming = false;
		touchedGroundOrPlayer = false;
		startPosition = transform.position;
		state = States.IDLE;
	}
	
	// Update is called once per frame
	void Update () {

		switch(state) {
			case States.IDLE:
				if(playerIsComming) {
					state = States.ATTACK;
				}
				break;
			case States.ATTACK:
				transform.Translate(Vector3.down * Time.deltaTime * downSpeed);

				if(touchedGroundOrPlayer) {
					state = States.RETREAT;
				}
				break;
			case States.RETREAT:
				transform.position = Vector3.MoveTowards(transform.position, startPosition, upSpeed*Time.deltaTime);

				if(transform.position == startPosition) {
					playerIsComming = false;
					touchedGroundOrPlayer = false;
					state = States.IDLE;
				}
				break;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.gameObject.tag == "Player") {
			playerIsComming = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Player") {
			touchedGroundOrPlayer = true;

			if(other.gameObject.tag == "Player") {
				Player player = other.gameObject.GetComponent<Player>();
				player.ApplyDamage(damagePoints);
			}
		}
	}
}
