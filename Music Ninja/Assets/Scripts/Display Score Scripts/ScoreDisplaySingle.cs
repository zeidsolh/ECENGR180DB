using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace MirrorBasics
{
    public class ScoreDisplaySingle : MonoBehaviour
    {
        GameObject inputObject;
        getInput inputScript;
        int score;
        public Text scoreText;
        public Text accuracyText;
        public int totalPossibleNotes;
        public TMP_Text endScreenText;
        int accuracy;
        

        void Start()
        {
            inputObject = GameObject.Find("Hitbox Trigger Box");
            inputScript = inputObject.GetComponent<getInput>();
            score = inputScript.playerScore;
            accuracy = inputScript.accuracy;
            Debug.Log("Score Display says score is: " + score);
        }

        void Update()
        {
            totalPossibleNotes = inputScript.possiblePoints;
            score = inputScript.playerScore;
            accuracy = inputScript.accuracy;
            //Player.localPlayer.UpdateScore(score);
            

            //Player.localPlayer.incrementScore(Player.localPlayer.playerIndex, score);

            //scoreText.text = score.ToString() + " / " + totalPossibleNotes;
            double percent = 0;
            if (totalPossibleNotes == 0)
            {
                percent = 0;
            }
            else
            {
                //was score
                percent = Convert.ToDouble(accuracy) / Convert.ToDouble(totalPossibleNotes);
            }
            percent = percent * 100;
            percent = Math.Truncate(percent);
            scoreText.text = score.ToString(); //+ " / " + totalPossibleNotes + " = " + percent.ToString() + "% Accuracy";
            accuracyText.text = accuracy.ToString()+ " / " + totalPossibleNotes + " = " + percent.ToString() + "%";
            endScreenText.text = "Your stats:\n" + "Score " + score.ToString() + "\n" + percent.ToString() + "% Accuracy";
        }

        void addScore()
        {
            score += 1;
        }

    }
}

