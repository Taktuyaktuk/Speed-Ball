using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGamesMenu : MonoBehaviour
{
    public void BasketBall()
    {
        SceneManager.LoadScene("Character Select");
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
