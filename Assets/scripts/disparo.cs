using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparo : MonoBehaviour {

	public Rigidbody bala;
	public Transform puntoDisparo;
	public float fuerzaDisparo=10;
	public float tiempoEntreDisparos = 0.15f;
	private float timer;

	//	public float speed = 6.0F;
	// public float jumpSpeed = 8.0F;
	// public float gravity = 20.0F;
	// private Vector3 moveDirection = Vector3.zero;

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

		timer += Time.deltaTime;
		if (Input.GetButton ("Fire1") && timer >= tiempoEntreDisparos && Time.timeScale != 0) {
			timer = 0f;
			Rigidbody balaInstance = Instantiate (bala, puntoDisparo.position, puntoDisparo.rotation) as Rigidbody;

			//balaInstance.velocity = fuerzaDisparo * puntoDisparo.forward;
			balaInstance.AddForce(puntoDisparo.forward * fuerzaDisparo,ForceMode.Impulse);
			rigidbody.AddForce(transform.forward * fuerzaDisparo,ForceMode.Impulse);
		}

	}


}