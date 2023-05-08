using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdatePowerUpBar : MonoBehaviour
{
    GameObject inputObject;
    getInput inputScript;
    public Image PowerUpBar;
    int streak;
    int powerups;
    int previousStreak;

    void Start()
    {
        inputObject = GameObject.Find("Hitbox Trigger Box");
        inputScript = inputObject.GetComponent<getInput>();
        streak = inputScript.streak;
        powerups=0;
        previousStreak=streak;
        PowerUpBar.fillAmount= 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        previousStreak=streak;
        streak = inputScript.streak;
        if(streak%2==0 && streak!=0 && streak !=previousStreak)
        {
            powerups++;
            
        }
        if(powerups>5)
        {
            powerups=5;
        }
        float fill =(float)powerups/5f;
        PowerUpBar.fillAmount = fill;
    }
}