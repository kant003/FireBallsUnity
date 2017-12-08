using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionBala : MonoBehaviour {
	public float m_MaxLifeTime = 2f;      
	SphereCollider theCollider;
	// Use this for initialization
	void Start () {
		theCollider = GetComponent<SphereCollider> ();
		Destroy(gameObject, m_MaxLifeTime);


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
    {
		Debug.Log("colision:"+other.name);
        theCollider.isTrigger = false;
		Destroy (gameObject, 0.3f);
    }

}
