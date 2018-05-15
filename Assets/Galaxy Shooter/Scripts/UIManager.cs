using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Sprite[] liveSprites;
	public Image livesImage;
	public Text scoreText;
	public Image title;
	public GameObject playerPrefab;

	public bool gameInProgress = false;

	private float score = 0;

	void Start() {
		title.enabled = true;
	}

	public void UpdateLives(int numberOfLives) {
		livesImage.sprite = liveSprites [numberOfLives];
	}

	public void UpdateScore() {
		score += 10;

		scoreText.text = "Score: " + score;
	}

	public void EnableMenuUI(bool enable) {
		if (enable) {
			gameInProgress = true;
			score = 0;
			scoreText.text = "Score: " + score;
			title.enabled = false;
		} else {
			gameInProgress = false;
			title.enabled = true;
		}
	}
}
