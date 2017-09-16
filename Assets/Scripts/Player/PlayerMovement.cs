using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100;

	void Awake(){ //Called even if script is disabled
		floorMask = LayerMask.GetMask("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	void Update(){ //Fires every frame update
	}

	void FixedUpdate(){ //Fires every physics update
		/* 
		 * GetAxis would have varying values from -1 to 1
		 * RawAxis would have only -1,0 and 1
		*/
		float h = Input.GetAxisRaw("Horizontal"); 
		float v = Input.GetAxisRaw("Vertical"); 

		Move (h, v);
		Turning ();
		Animating (h, v);
	}

	void Move(float h, float v){
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime; 
		// Normalized because the vector when moved diagonally will be faster when both h+v
		//Delta time = time between each update call;

		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning(){
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		// TAkes a point on screen and casts a ray forward onto the scene
		// point given is mouse position

		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			//floorHit.point = point it hits the floor

			playerToMouse.y = 0f;

			//Quaternion used to store rotation. Vector3 cannot store rotation
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			//Z axis is default Forward axis for most model. To make playerToMouse as default forward vector we will use LookRotation
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	void Animating(float h, float v){
		bool walking = (h != 0f || v != 0f);
		anim.SetBool ("IsWalking", walking);
	}
}
