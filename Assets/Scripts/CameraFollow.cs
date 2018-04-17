using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public Vector3 offset = new Vector3(1f, 1f, -10f);

	// Use this for initialization
	void Start () {
		if(target == null) {
			target = GameObject.FindGameObjectWithTag("Player").transform;
		}

		if(target == null) {
			Debug.LogError("Could not find Player");
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, offset.z);
	}
}
