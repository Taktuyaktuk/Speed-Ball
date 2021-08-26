using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SkinManager : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> skins = new List<Sprite>();
    private int selectedSkin = 0;
    public GameObject playerskin;

    public void NextOption()
    {
        selectedSkin = selectedSkin + 1;
        if(selectedSkin == skins.Count)
        {
            selectedSkin = 0;
        }
        sr.sprite = skins[selectedSkin];
    }

    public void BackOption()
    {
        selectedSkin = selectedSkin - 1;
        if(selectedSkin <0)
        {
            selectedSkin = skins.Count - 1;
        }
        sr.sprite = skins[selectedSkin];
    }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("skinIndex", selectedSkin);
        SceneManager.LoadScene("Game");
    }
}
