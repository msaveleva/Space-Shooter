using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Sprite[] liveSprites;
	public Image livesImage;
	public Text scoreText;
    public Text bestScoreText;
	public Image title;
    public GameObject pauseMenu;
	public GameObject playerPrefab; //TODO: remove

    private Animator _pauseMenuAnimatior;

	public bool gameInProgress = false;

	private float score = 0;
    private float bestScore = 0; //TODO: load from DB.

	void Start() {
        _pauseMenuAnimatior = GameObject.Find("Pause_Panel").GetComponent<Animator>();
        _pauseMenuAnimatior.updateMode = AnimatorUpdateMode.UnscaledTime;

		title.enabled = true;
	}

	public void UpdateLives(int numberOfLives) {
		livesImage.sprite = liveSprites [numberOfLives];
	}

	public void UpdateScore() {
		score += 10;

		scoreText.text = "Score: " + score;
	}

    public void CheckBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            bestScoreText.text = "Best: " + bestScore;
            //TODO: update bestScore in DB.
        }
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

    public void EnablePauseUI(bool enable)
    {
        pauseMenu.SetActive(enable);

        if (enable)
        { 
            _pauseMenuAnimatior.SetBool("isPaused", true);
        }
    }
}
