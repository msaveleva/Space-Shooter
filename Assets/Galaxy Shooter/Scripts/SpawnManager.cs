using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	private float kScreenHeightBorder = 6.67f;
	private float kScreenWidthBorder = 7.46f;

	private float _enemySpawnTimeout = 2.0f;
	private float _powerupSpawnTimeout = 5.0f;

	[SerializeField]
	private GameObject enemyPrefab;
	[SerializeField]
	private GameObject[] powerups;

	private GameManager _gameManager;

	// Use this for initialization
	void Start () {
		_gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	public void StartSpawn() {
		StartCoroutine (_CuntdownEnemySpawn ());
		StartCoroutine (_CountdownPowerupSpawn ());
	}

	private IEnumerator _CuntdownEnemySpawn() {
		while (_gameManager != null && _gameManager.gameInProgress) {
			Instantiate (enemyPrefab, new Vector3(Random.Range (-kScreenWidthBorder, kScreenWidthBorder), kScreenHeightBorder, transform.position.z), Quaternion.identity);

			yield return new WaitForSeconds (_enemySpawnTimeout);
		}
	}

	private IEnumerator _CountdownPowerupSpawn() {
		while (_gameManager != null && _gameManager.gameInProgress) {
			int randomType = Random.Range (0, 3);
			Instantiate (powerups [randomType], new Vector3 (Random.Range (-kScreenWidthBorder, kScreenWidthBorder), kScreenHeightBorder, transform.position.z), Quaternion.identity);

			yield return new WaitForSeconds (_powerupSpawnTimeout);
		}
	}
}
