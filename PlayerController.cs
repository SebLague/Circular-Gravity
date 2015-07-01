using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GravityBody))]
public class PlayerController : MonoBehaviour {
	
	// public vars
	public float walkSpeed = 6;
	public float jumpForce = 220;
	public LayerMask groundedMask;
	
	// System vars
	bool grounded;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	Rigidbody2D rigidbody;
	GravityBody gravityBody;

	float targetRotation;
	float rotSmoothV;
	
	void Awake() {
		rigidbody = GetComponent<Rigidbody2D> ();
		gravityBody = GetComponent<GravityBody> ();
	}
	
	void Update() {
		
		// Calculate movement:
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = 0;
		
		Vector3 moveDir = new Vector3(inputX,0, inputY).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f);
		
		// Jump
		if (Input.GetButtonDown("Jump")) {
			if (grounded) {
				rigidbody.AddForce(transform.up * jumpForce);
			}
		}
		
		// Grounded check
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit2D hit = Physics2D.Raycast((Vector2)(transform.position - transform.up * .5f), -(Vector2)transform.up,10f,groundedMask);
		if (hit) {
			grounded = true;
			/*
			Vector3 gravityDir = (transform.position - gravityBody.gravityCentre).normalized;
			Vector3 surfaceDir = new Vector3(hit.normal.x,hit.normal.y);
			float angleBetween = Vector3.Angle(gravityDir,surfaceDir);
		
			float surfaceRot = (Mathf.Atan2(hit.normal.y,hit.normal.x) * Mathf.Rad2Deg - 90 + 360) % 360;
			float gravityRot = (Mathf.Atan2(gravityDir.y,gravityDir.x) * Mathf.Rad2Deg - 90 + 360) % 360;


			if (angleBetween < 60) {
				targetRotation = surfaceRot;
			}
			else {
				targetRotation = gravityRot;
			}



			transform.eulerAngles = Vector3.forward * targetRotation;
			*/
			//transform.eulerAngles = Vector3.forward * Mathf.SmoothDamp(transform.eulerAngles.z,targetRotation,ref rotSmoothV,.1f);

			//print (surfaceRot + "  " + gravityRot);
		}
		else {
			grounded = false;
		}
		
	}
	
	void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		Vector2 newPos = rigidbody.position + (Vector2)localMove;
		rigidbody.position = newPos;
	}
}