using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public GameObject selectedSkin;
    public GameObject body;

    private Sprite playerSprite;
    void Start()
    {
        PlayerPrefs.GetInt("skinIndex");

        playerSprite = selectedSkin.GetComponent<SpriteRenderer>().sprite;

        body.GetComponent<SpriteRenderer>().sprite = playerSprite;
    }
}
