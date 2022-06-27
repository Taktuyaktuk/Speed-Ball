using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.ComponentModel;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance1;
    public TextMeshProUGUI text;
    int score ;


    // Start is called before the first frame update
    void Start()
    {
        if (instance1 == null)
        {
            instance1 = this;
        }
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = "X" + score.ToString();
    }

   
}
