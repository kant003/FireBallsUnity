using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour {
	private float verticalVelocity;
	private float gravity = 20.0f;
	private float jumpForce = 4.0f;
	public float speed = 6.0F;

	CharacterController controller;
	Rigidbody rigidbody;
	Transform transform;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		rigidbody = GetComponent<Rigidbody> ();
		transform = GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		if (controller.isGrounded) {
			verticalVelocity = -gravity * Time.deltaTime;
			if (Input.GetKeyDown (KeyCode.Space)) {
				verticalVelocity = jumpForce;
			}
		} else {
			verticalVelocity -= gravity * Time.deltaTime;

		}
		Vector3 moveVector = Vector3.zero;
		moveVector.x = -Input.GetAxis ("Horizontal");
		moveVector.y = verticalVelocity;
		moveVector.z = 0;
		moveVector *= speed;

		controller.Move (moveVector * Time.deltaTime);

		//Mouse Position in the world. It's important to give it some distance from the camera. 
		//If the screen point is calculated right from the exact position of the camera, then it will
		//just return the exact same position as the camera, which is no good.
		//  Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
		//mouseWorldPosition.z=transform.position.z;
		//Angle between mouse and this object
		//  float angle = AngleBetweenPoints(transform.position, mouseWorldPosition);
		//   Debug.DrawLine(transform.position, mouseWorldPosition, Color.red);
		//Ta daa
		//   transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle));

		//	Vector3 relativePos = mouseWorldPosition - transform.position;

		// Generate a plane that intersects the transform's position with an upwards normal.
		Plane playerPlane = new Plane (Vector3.forward, transform.position);
		DrawPlane(transform.position, Vector3.forward);
		// Generate a ray from the cursor position
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Determine the point where the cursor ray intersects the plane.
		// This will be the point that the object must look towards to be looking at the mouse.
		// Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
		//   then find the point along that ray that meets that distance.  This will be the point
		//   to look at.
		float hitdist = 0.0f;
		// If the ray is parallel to the plane, Raycast will return false.
		if (playerPlane.Raycast (ray, out hitdist)) {
			// Get the point along the ray that hits the calculated distance.
			Vector3 targetPoint = ray.GetPoint (hitdist);
			Debug.DrawLine (transform.position, targetPoint, Color.red);
			// Determine the target rotation.  This is the rotation if the transform looks at the target point.
			//Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);

			// Smoothly rotate towards the target point.
			//transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, speed * Time.deltaTime);


			 Vector3 relativePos = targetPoint - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
		}

	}

	float AngleBetweenPoints (Vector2 a, Vector2 b) {
		return Mathf.Atan2 (a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}

	void DrawPlane (Vector3 position , Vector3 normal) {

		Vector3 v3;

		if (normal.normalized != Vector3.forward)
			v3 = Vector3.Cross (normal, Vector3.forward).normalized * normal.magnitude;
		else
			v3 = Vector3.Cross (normal, Vector3.up).normalized * normal.magnitude;;

		Vector3 corner0 = position + v3;
		Vector3 corner2 = position - v3;
		Quaternion q = Quaternion.AngleAxis (90.0f, normal);
		v3 = q * v3;
		Vector3 corner1 = position + v3;
		Vector3 corner3 = position - v3;

		Debug.DrawLine (corner0, corner2, Color.green);
		Debug.DrawLine (corner1, corner3, Color.green);
		Debug.DrawLine (corner0, corner1, Color.green);
		Debug.DrawLine (corner1, corner2, Color.green);
		Debug.DrawLine (corner2, corner3, Color.green);
		Debug.DrawLine (corner3, corner0, Color.green);
		Debug.DrawRay (position, normal, Color.red);
	}

}