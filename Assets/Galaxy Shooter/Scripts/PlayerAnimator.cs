using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

	private Animator _animator;

    private bool _isPlayerOne = false;
    private GameManager _gameManager;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Player playerComponent = gameObject.GetComponent<Player>();
        _isPlayerOne = playerComponent.isPlayerOne;
	}
	
	// Update is called once per frame
	void Update () {
        bool leftInputDown = false;
        bool leftInputUp = false;
        bool rightInputDown = false;
        bool rightInputUp = false;

        if (!_gameManager.isCoOpMode)
        {
            leftInputDown = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
            leftInputUp = Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow);
            rightInputDown = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
            rightInputUp = Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow);
        }
        else if (_isPlayerOne)
        {
            leftInputDown = Input.GetKeyDown(KeyCode.A);
            leftInputUp = Input.GetKeyUp(KeyCode.A);
            rightInputDown = Input.GetKeyDown(KeyCode.D);
            rightInputUp = Input.GetKeyUp(KeyCode.D);
        }
        else
        {
            leftInputDown = Input.GetKeyDown(KeyCode.LeftArrow);
            leftInputUp = Input.GetKeyUp(KeyCode.LeftArrow);
            rightInputDown = Input.GetKeyDown(KeyCode.RightArrow);
            rightInputUp = Input.GetKeyUp(KeyCode.RightArrow);
        }

		if (leftInputDown) {
			_animator.SetBool ("Turn_Left", true);
			_animator.SetBool ("Turn_Right", false);
		} else if (leftInputUp) {
			_animator.SetBool ("Turn_Left", false);
			_animator.SetBool ("Turn_Right", false);
		} 

		if (rightInputDown) {
			_animator.SetBool ("Turn_Right", true);
			_animator.SetBool ("Turn_Left", false);
		} else if (rightInputUp) {
			_animator.SetBool ("Turn_Left", false);
			_animator.SetBool ("Turn_Right", false);
		}
	}
}
