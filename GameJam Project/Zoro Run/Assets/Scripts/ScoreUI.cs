using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    [HideInInspector] public int numOfCoins;
    public TMP_Text coins;
    public TMP_Text UIscore;
    public TMP_Text FinalCoins;
    public TMP_Text FinalScore;
    private int score;
    [HideInInspector] public int speed;
    public Transform player;
    void Start()
    {
        score = 0;
        speed = 1;
        numOfCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score = (int)player.position.z; 
        coins.text = numOfCoins.ToString();
        UIscore.text = score.ToString("0");
        FinalCoins.text = numOfCoins.ToString();
        FinalScore.text = score.ToString("0");

    }
}
