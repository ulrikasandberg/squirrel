using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[Range(0.0f, 10.0f)]
	public float speed = 3f;
	public float jumpForce = 300f;
	public int healthPoints = 10;

	private Rigidbody2D rigidBody;
	private bool isGrounded = false;
	private bool isRunning = false;
	private bool canPull = false;
	private float moveX, moveY;
	private Animator animator;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		if(rigidBody == null) {
			Debug.LogError("Rigidbody2D missing from GameObject");
		}

		animator = GetComponent<Animator> ();
		if(animator == null) {
			Debug.LogError("Animator missing from GameObject");
		}
	}
	
	// Update is called once per frame
	void Update () {
		moveX = Input.GetAxis("Horizontal");
		moveY = rigidBody.velocity.y;

		if(isGrounded && Input.GetButtonDown("Jump")) {
			Jump();
			isGrounded = false;
		}

		if(moveX != 0) {
			rigidBody.velocity = new Vector2(moveX*speed, moveY);
			isRunning = true;
		} else {
			isRunning = false;
		}
		animator.SetBool("isRunning", isRunning);
		animator.SetBool("isGrounded", isGrounded);
	}

	void LateUpdate () {
		Vector3 localScale = transform.localScale;
		bool facingLeft = false;

		if(moveX < 0) {
			facingLeft = true;
		} else if(moveX > 0) {
			facingLeft = false;
		}

		if((facingLeft && localScale.x > 0) || (!facingLeft && localScale.x < 0)) {
			localScale.x *= -1;
		}

		transform.localScale = localScale;
	}

	void Jump() {
		moveY = 0f;
		float direction = 0;
		if(moveX != 0) {
			direction = (moveX < 0) ? jumpForce * -1f : jumpForce;
		}
		rigidBody.AddForce(new Vector2(direction, jumpForce));
	}

	void OnCollisionEnter2D(Collision2D coll) {

		/*float contactY = coll.contacts[0].point.y;
		float maxY = coll.collider.bounds.max.y;
		float minY = coll.collider.bounds.min.y;
		float test = transform.position.y;

		if(test > minY && test < maxY && Input.GetButtonDown("Jump")) {
			Debug.Log("Sidan");
		}*/

		isGrounded = (coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Item") ? true : false;

		if(coll.gameObject.tag == "Collectible") {
			Destroy(coll.gameObject);
		}
	}

	public void ApplyDamage(int damage) {
		healthPoints -= damage;
		Debug.Log(healthPoints);
		if(healthPoints <= 0) {
			// TODO: Game over!
		}
	}
}
