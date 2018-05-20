using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool isCoOpMode = false;
    public bool gameInProgress = false;
    public bool gameOnPause = false;

    [SerializeField]
	private GameObject playerPrefab;
    [SerializeField]
    private GameObject coOpPlayersPrefab;

	private UIManager _uiManager;
	private SpawnManager _spawnManager;

	// Use this for initialization
	void Start () {
		_uiManager = GameObject.Find ("Canvas").GetComponent<UIManager> ();
		_spawnManager = GameObject.Find ("SpawnManager").GetComponent<SpawnManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameInProgress) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				StartGame ();
			}

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OpenMainMenu();
            }
		}
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _EnablePause(!gameOnPause);
            }
        }
	}

    private void _EnablePause(bool enable)
    {
        gameOnPause = enable;
        Time.timeScale = enable ? 0 : 1;
        _uiManager.EnablePauseUI(enable);
    }

    public void ResumeGame()
    {
        _EnablePause(false);
    }

    public void OpenMainMenu()
    {
        if (gameOnPause)
        {
            ResumeGame();
        }
        
        SceneManager.LoadScene("Main_menu");
    }

	public void StartGame() {
		gameInProgress = true;
        if (!isCoOpMode)
        {
            Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(coOpPlayersPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }

		_uiManager.EnableMenuUI (true);
		_spawnManager.StartSpawn ();
	}

	public void GameOver() {
		gameInProgress = false;
		_uiManager.EnableMenuUI (false);
	}
}
