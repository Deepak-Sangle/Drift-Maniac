using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    public int numOfCoins;
    public TMP_Text coins;
    public TMP_Text UIscore;
    private int score;
    public int speed;
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

    }
}
