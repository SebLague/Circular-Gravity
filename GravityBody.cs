using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class GravityBody : MonoBehaviour {
	
	GravityAttractor planet;
	Rigidbody2D rigidbody;

	[HideInInspector]
	public Vector3 gravityCentre;

	void Awake () {
		planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
		rigidbody = GetComponent<Rigidbody2D> ();

		// Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
		rigidbody.gravityScale = 0;
		rigidbody.fixedAngle = true;

		gravityCentre = planet.transform.position;
	}
	
	void FixedUpdate () {
		// Allow this body to be influenced by planet's gravity
		planet.Attract(rigidbody);
	}
}