using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	//Constants.
	private float kHalfScreenWidth = 9.0f;
	private float kHeightRestriction = -4.2f;
	private float kEnhancedSpeedMultiplier = 2.5f;

	//Public variables.


	//Private customizable variables.
	[SerializeField]
	private GameObject _laserPrefab;
	[SerializeField]
	private GameObject _tripleShot;
	[SerializeField]
	private GameObject _explosion;
	[SerializeField]
	private GameObject _shieldGameObject;
	[SerializeField]
	private GameObject[] _engineFailures;

	[SerializeField]
	private float _speed = 5.0f;
	[SerializeField]
	private int _maxNumberOfLifes = 3;
	[SerializeField]
	private float _shootCooldown = 0.25f;

	[SerializeField]
	private bool _canTripleShot = false;
	[SerializeField]
	private float _tripleShotEnableTime = 5.0f;
	[SerializeField]
	private bool _hasEnhancedSpeed = false;
	[SerializeField]
	private float _enhancedSpeedEnableTime = 5.0f;
	[SerializeField]
	private bool _hasRaisedShield = false;

	private UIManager _uiManager;
	private GameManager _gameManager;
	private AudioSource _audioSource;

	//Private variables.
	private float _nextTimeCounter = 0.0f;
	private int _currentNumberOfLifes = 5; //some initial value.

	void Start () {
		transform.position = new Vector3 (0,0,0);
		_currentNumberOfLifes = _maxNumberOfLifes;

		_audioSource = GetComponent<AudioSource> ();
		_gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		_uiManager = GameObject.Find ("Canvas").GetComponent<UIManager> ();
		if (_uiManager != null) {
			_uiManager.UpdateLives (_currentNumberOfLifes);
		}
	}

	void Update () {
		movePlayer();
		shootWithLaser ();
	}

	public void ReduceLife() {
		if (_hasRaisedShield) {
			_hasRaisedShield = false;
			_shieldGameObject.SetActive (false);
			return;
		}

		_currentNumberOfLifes -= 1;

		//Show randomized engine damage.
		{
			GameObject engineFailure;
			do {
				engineFailure = _engineFailures [Random.Range (0, 2)];
			} while (_currentNumberOfLifes > 0 && engineFailure.activeSelf);
			engineFailure.SetActive (true);
		}

		//Updating UI. 
		if (_uiManager != null) {
			_uiManager.UpdateLives (_currentNumberOfLifes);
		}
			
		if (_currentNumberOfLifes <= 0) {
			Instantiate (_explosion, transform.position, Quaternion.identity);

			if (_gameManager != null) {
				_gameManager.GameOver ();
			}

 			Destroy (this.gameObject);
		}
	}

	private void movePlayer() {
		//User input.
		float horizontalInput = Input.GetAxis ("Horizontal");
		float verticalInput = Input.GetAxis ("Vertical");

		float adjustedSpeed = _speed;
		if (_hasEnhancedSpeed) {
			adjustedSpeed *= kEnhancedSpeedMultiplier;
		}
		transform.Translate (Vector3.right * horizontalInput * adjustedSpeed * Time.deltaTime);
		transform.Translate (Vector3.up * verticalInput * adjustedSpeed * Time.deltaTime);


		//Borders warp.
		//TODO: fix issue with constant warp in some points. 
		float xPosition = transform.position.x;
		float yPosition = transform.position.y;

		if (xPosition > kHalfScreenWidth || xPosition < -kHalfScreenWidth) {
			xPosition = -xPosition;
		}

		if (yPosition > 0) {
			yPosition = 0;
		} else if (yPosition < kHeightRestriction) {
			yPosition = kHeightRestriction;
		}

		if (xPosition != transform.position.x || yPosition != transform.position.y) {
			transform.position = new Vector3 (xPosition, yPosition, 0);		
		}
	}

	private void shootWithLaser() {
		bool shootKeyPressed = Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Mouse0);
		bool canShoot = Time.time > _nextTimeCounter;
		if (shootKeyPressed && canShoot) {
			_audioSource.Play ();

			_nextTimeCounter = Time.time + _shootCooldown;

			if (_canTripleShot) {
				Instantiate (_tripleShot, transform.position, Quaternion.identity);
			} else {
				Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.02f, 0), Quaternion.identity);
			}
		}
	}


	//Powerups methods

	public void enableTripleShot() {
		_canTripleShot = true;
		StartCoroutine(_tripleShotCountdown());

//		Debug.Log ("Tripleshot activated.");
	}

	//TODO: fix possible issue when disable second powerup while it should be active. 
	private IEnumerator _tripleShotCountdown() {
		yield return new WaitForSeconds(_tripleShotEnableTime);
		_canTripleShot = false;

//		Debug.Log ("Tripleshot deactivated.");
	}

	public void enhanceSpeed() {
		_hasEnhancedSpeed = true;
		StartCoroutine(_enhancedSpeedCountdown());

//		Debug.Log ("Moving speed restored to default.");
	}

	private IEnumerator _enhancedSpeedCountdown() {
		yield return new WaitForSeconds (_enhancedSpeedEnableTime);
		_hasEnhancedSpeed = false;

//		Debug.Log ("Moving speed enhansed.");
	}

	public void raiseShield() {
		_hasRaisedShield = true;
		_shieldGameObject.SetActive (true);
//		Debug.Log ("Shield up.");
	}
}
