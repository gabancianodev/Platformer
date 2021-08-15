using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public void PlayGame()
    {
        SceneManager.LoadScene("game");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }

    public void GoToLevelSuccess()
    {
        SceneManager.LoadScene("levelsuccess");
    }

    public void GoToGameOver()
    {
        SceneManager.LoadScene("gameover");
    }
}
