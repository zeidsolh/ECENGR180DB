/*
Description: 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class score_display_v2 : MonoBehaviour
{
    public static score_display_v2 instance;

    GameObject inputObject;
    getInput inputScript;
    int score = 0;
    public Text score_Text;
    public TMP_Text oppEndScreenText;
    public int totalPossibleNotes;

    private void Awake()
    {

        instance = this;
    }

    void Start()
    {
        inputObject = GameObject.Find("Hitbox Trigger Box");
        inputScript = inputObject.GetComponent<getInput>();
        score_Text.text = score.ToString();
    }

    void Update()
    {
    }

    public void UpdatePoint(int score1)
    {
        totalPossibleNotes = inputScript.possiblePoints / 4;
        score_Text.text = score1.ToString();
        double percent = 0;
        if (totalPossibleNotes == 0)
        {
            percent = 0;
        }
        else
        {
            percent = Convert.ToDouble(score1) / Convert.ToDouble(totalPossibleNotes);
        }
        percent = percent * 100;
        percent = Math.Truncate(percent);
        oppEndScreenText.text = "Opponent stats:\n" + "Score " + score1.ToString() + "\n" + percent.ToString() + "% Accuracy";
    }


}