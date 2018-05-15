using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	//Constants.
	private float kScreenTopBorder = 6.0f;

	//Private customizable variables. 
	[SerializeField]
	private float _speed = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.up * _speed * Time.deltaTime);

		if (transform.position.y >= kScreenTopBorder) {
			DestroyLaser ();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy") {
			DestroyLaser ();
		}
	}


	//Private methods. 
	private void DestroyLaser() {
		if (transform.parent != null && transform.parent.gameObject != null) {
			Destroy (transform.parent.gameObject);
		} else {
			Destroy (this.gameObject);
		}
	}
}
