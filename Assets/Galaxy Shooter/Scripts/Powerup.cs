using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

	public enum PowerType:int {
		TripleShot,
		Speed,
		Shield
	}

	private float kScreenHeightBorder = 6.67f;

	//Private variables.
	[SerializeField]
	private float _speed = 3.0f;
	[SerializeField]
	private PowerType type;
	[SerializeField]
	private AudioClip _powerupAudioClip;

	void Update () {
		transform.Translate (Vector3.down * _speed * Time.deltaTime);

		if (transform.position.y < -kScreenHeightBorder) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag != "Player") { //Set tag to any object from the top menu in Inspector.
			return;
		}

		Player player = other.GetComponent<Player> ();

		if (player != null) {
			AudioSource.PlayClipAtPoint (_powerupAudioClip, Camera.main.transform.position, 1.0f);

			switch (type) {
			case PowerType.TripleShot:
				player.enableTripleShot ();
				break;
			case PowerType.Speed:
				player.enhanceSpeed ();
				break;
			case PowerType.Shield:
				player.raiseShield ();
				break;
			default:
				break;
			}
		}

		Destroy (this.gameObject);
	}
}
