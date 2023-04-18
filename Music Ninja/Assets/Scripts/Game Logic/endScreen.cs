using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

namespace MirrorBasics
{
    public class endScreen : MonoBehaviour
    {
        GameObject inputObject;
        getInput inputScript;
        int score;
        public TMP_Text scoreText;
        public int totalPossibleNotes;
        Button continueButton;

        public TMP_Text highScoreText;

        void Start()
        {
            inputObject = GameObject.Find("Hitbox Trigger Box");
            inputScript = inputObject.GetComponent<getInput>();
            score = inputScript.playerScore;
            if(score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            highScoreText.text = "High Score = " + PlayerPrefs.GetInt("HighScore", 0).ToString();
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

        public void ContinueFunction()
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
}
