using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing = 5f;

	Vector3 offest;

	void Start(){
		offest = transform.position - target.position;
	}

	void FixedUpdate(){
		//To follow a phyisic object
		//If we used Update() instead it would move in a different time

		Vector3 targetCamPos = target.position + offest;
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime); 
		//lerp helps smothly transition
	}
}
