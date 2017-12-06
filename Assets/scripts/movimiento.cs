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
		controller = GetComponent<CharacterController>();
		rigidbody = GetComponent<Rigidbody>();
		transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if(controller.isGrounded){
			verticalVelocity = -gravity * Time.deltaTime;
			if(Input.GetKeyDown(KeyCode.Space)){
				verticalVelocity = jumpForce;
			}
		}else{
			verticalVelocity -=gravity * Time.deltaTime;
 
		}
		Vector3 moveVector = Vector3.zero;
		moveVector.x = -Input.GetAxis("Horizontal");
		moveVector.y = verticalVelocity;
		moveVector.z = 0;
		moveVector *= speed;

		controller.Move(moveVector * Time.deltaTime);


		         //Mouse Position in the world. It's important to give it some distance from the camera. 
         //If the screen point is calculated right from the exact position of the camera, then it will
         //just return the exact same position as the camera, which is no good.
         Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
         
         //Angle between mouse and this object
         float angle = AngleBetweenPoints(transform.position, mouseWorldPosition);
          Debug.DrawLine(transform.position, mouseWorldPosition, Color.red);
         //Ta daa
         transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle));
     }
     
     float AngleBetweenPoints(Vector2 a, Vector2 b) {
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
     }
	
}
