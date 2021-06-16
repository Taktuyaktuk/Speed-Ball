using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Adventure()
    {
        SceneManager.LoadScene("Character Select");
    }

    public void MiniGames()
    {
        SceneManager.LoadScene("MiniGames Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
