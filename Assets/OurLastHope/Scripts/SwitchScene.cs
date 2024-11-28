using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void playgame()
    {
        SceneManager.LoadScene("MovementTest");
    }
    public void Optionsgame()
    {
        SceneManager.LoadScene("OptionScene");
    }
    public void Creditsscene()
    {
        SceneManager.LoadScene("creditsscene");
    }
    public void Quitgame()
    {
        Application.Quit();
    }

    public void backgame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
