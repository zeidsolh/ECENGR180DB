using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace MirrorBasics
{
    public class ScoreDisplay : MonoBehaviour
    {
        GameObject inputObject;
        getInput inputScript;
        int score;
        public Text scoreText;
        public int totalPossibleNotes;
        public TMP_Text myEndScreenText;

        void Start()
        {
            inputObject = GameObject.Find("Hitbox Trigger Box");
            inputScript = inputObject.GetComponent<getInput>();
            score = inputScript.playerScore;
            Debug.Log("Score Display says score is: " + score);
        }

        void Update()
        {
            totalPossibleNotes = inputScript.possiblePoints / 4;
            score = inputScript.playerScore;
            Player.localPlayer.UpdateScore(score);
            

            //Player.localPlayer.incrementScore(Player.localPlayer.playerIndex, score);

            //scoreText.text = score.ToString() + " / " + totalPossibleNotes;
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
            scoreText.text = score.ToString() + " / " + totalPossibleNotes + " = " + percent.ToString() + "% Accuracy";
            myEndScreenText.text = "Your stats:\n" + "Score " + score.ToString() + "\n" + percent.ToString() + "% Accuracy";
        }

        void addScore()
        {
            score += 1;
        }

    }
}

