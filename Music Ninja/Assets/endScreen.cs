using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace MirrorBasics
{
    public class endScreen : MonoBehaviour
    {
        GameObject inputObject;
        getInput inputScript;
        int score;
        public TMP_Text scoreText;
        public int totalPossibleNotes;

        void Start()
        {
            inputObject = GameObject.Find("Hitbox Trigger Box");
            inputScript = inputObject.GetComponent<getInput>();
            score = inputScript.playerScore;
        }

        void Update()
        {
            totalPossibleNotes = inputScript.possiblePoints / 4;
            score = inputScript.playerScore;
            double percent = 0;
            if (totalPossibleNotes == 0)
            {
                percent = 0;
            }
            else
            {
                percent = Convert.ToDouble(score) / Convert.ToDouble(totalPossibleNotes);
            }
            percent = percent * 100;
            percent = Math.Truncate(percent);
            scoreText.text = "Player Name\n" + "Score " + score.ToString() + "\n" + percent.ToString() + "% Accuracy";
        }

    }
}
