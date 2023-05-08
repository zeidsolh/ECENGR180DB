using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdateStreakGraphics : MonoBehaviour
{
    GameObject canvas;
    static streakScore streakScript;
    public Image streakBar;
    public Image PowerUpBar;
    int streak;
    int powerups;

    void Start()
    {
        canvas = GameObject.Find("UICanvas");
        streakScript = canvas.GetComponent<streakScore>();
        streakScript = streakScript;
        streak = streakScript.streak;
        powerups=streakScript.powerUps;
        PowerUpBar.fillAmount= 0f;
        streakBar.fillAmount= 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
    
        powerups=streakScript.powerUps;
        streak=streakScript.streak;
        Debug.Log(streak);
        if(powerups>5)
        {
            powerups=5;
        }
        float fillPowerup =(float)powerups/5f;
        streak=streak%2;
        float fillStreak =(float)streak/2f;
        PowerUpBar.fillAmount = fillPowerup;
        streakBar.fillAmount = fillStreak;
    }
}