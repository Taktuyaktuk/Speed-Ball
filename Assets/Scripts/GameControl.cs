using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public GameObject[] characters;
    public Transform playerStartPosition;
    public string menuScene = "Character Selection Menu";
    private string selectedCharacterDataName = "SelectedCharacter";
    int selectedCharacter;
    public GameObject playerObject;

    private void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName, 0);
        playerObject = Instantiate(characters[selectedCharacter], playerStartPosition);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
           
        }
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}
