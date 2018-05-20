using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlaySinglePlayerGame()
    {
        SceneManager.LoadScene("Single_player");
    }

    public void PlayCoOpModeGame()
    {
        SceneManager.LoadScene("Co-Op_mode");
    }
}
