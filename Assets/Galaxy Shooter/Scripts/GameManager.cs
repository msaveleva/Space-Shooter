using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool isCoOpMode = false;
    public bool gameInProgress = false;
	public GameObject playerPrefab;

	private UIManager _uiManager;
	private SpawnManager _spawnManager;

	// Use this for initialization
	void Start () {
		_uiManager = GameObject.Find ("Canvas").GetComponent<UIManager> ();
		_spawnManager = GameObject.Find ("SpawnManager").GetComponent<SpawnManager> ();

        isCoOpMode = SceneManager.GetActiveScene().name == "Co-Op_mode";
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameInProgress) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				StartGame ();
			}
		}
	}

	public void StartGame() {
		gameInProgress = true;
		Instantiate (playerPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
		_uiManager.EnableMenuUI (true);
		_spawnManager.StartSpawn ();
	}

	public void GameOver() {
		gameInProgress = false;
		_uiManager.EnableMenuUI (false);
	}
}
