using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	//Constants.
	private float kScreenHeightBorder = 6.67f;
	private float kScreenWidthBorder = 7.46f;

	[SerializeField]
	private float _speed = 3.0f;
	[SerializeField]
	private GameObject _enemyExplosion;
	[SerializeField]
	private AudioClip _explodeAudioClip;

	private UIManager _uiManager;

	// Use this for initialization
	void Start () {
		_uiManager = GameObject.Find ("Canvas").GetComponent <UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.down * _speed * Time.deltaTime);

		if (transform.position.y < -kScreenHeightBorder) {
			_MoveAtRandomTopPosition ();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		//TODO: fix
		if (other.tag == "Player") {
			Player player = other.GetComponent<Player> ();
			if (player == null) {
				return;
			}

			player.ReduceLife ();
			Instantiate (_enemyExplosion, transform.position, Quaternion.identity);
			_uiManager.UpdateScore ();
			AudioSource.PlayClipAtPoint (_explodeAudioClip, Camera.main.transform.position, 1.0f);
			Destroy (this.gameObject);
		} else if (other.tag == "Laser") {
			Instantiate (_enemyExplosion, transform.position, Quaternion.identity);
			_uiManager.UpdateScore ();
			AudioSource.PlayClipAtPoint (_explodeAudioClip, Camera.main.transform.position, 1.0f);
			Destroy (this.gameObject);
		}
	}

	//Private methods.

	private void _MoveAtRandomTopPosition() {
		float randomXValue = Random.Range (-kScreenWidthBorder, kScreenWidthBorder);
		transform.position = new Vector3 (randomXValue, kScreenHeightBorder, transform.position.z);
	}
}
