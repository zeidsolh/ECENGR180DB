/*
Description: 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace MirrorBasics
{
    public class score_display_v2 : MonoBehaviour
    {
        public static score_display_v2 instance;

        GameObject inputObject;
        getInput inputScript;
        int score = 0;
        double percent = 0;
        public Text score_Text;
        public Text mult_score_Text;
        public TMP_Text oppEndScreenText;
        public int totalPossibleNotes = 0;
        int rawScore = 0;

        private void Awake()
        {

            instance = this;
        }

        void Start()
        {
            inputObject = GameObject.Find("Hitbox Trigger Box");
            inputScript = inputObject.GetComponent<getInput>();
            score_Text.text = rawScore.ToString() + " / " + totalPossibleNotes.ToString() + " = " + percent.ToString() + "%";
            mult_score_Text.text = score.ToString();
        }

        void Update()
        {
        }

        public void UpdatePoint(int score1)
        {
            score = score1;
            mult_score_Text.text = score.ToString();
            if (totalPossibleNotes == 0)
            {
                percent = 0;
            }
            else
            {
                percent = Convert.ToDouble(rawScore) / Convert.ToDouble(totalPossibleNotes);
            }
            percent = percent * 100;
            percent = Math.Truncate(percent);
            score_Text.text = rawScore.ToString() + " / " + totalPossibleNotes.ToString() + " = " + percent.ToString() + "%";
            oppEndScreenText.text = "Opponent stats:\n" + "Score " + score.ToString() + "\n" + percent.ToString() + "% Accuracy";
            Debug.Log($"Percent{percent}");
        }

        public void UpdatePossiblePoint(int score2)
        {
            totalPossibleNotes = score2;
            mult_score_Text.text = score.ToString();
            if (totalPossibleNotes == 0)
            {
                percent = 0;
            }
            else
            {
                percent = Convert.ToDouble(rawScore) / Convert.ToDouble(totalPossibleNotes);
            }
            percent = percent * 100;
            percent = Math.Truncate(percent);
            score_Text.text = rawScore.ToString() + " / " + totalPossibleNotes.ToString() + " = " + percent.ToString() + "%";
            oppEndScreenText.text = "Opponent stats:\n" + "Score " + score.ToString() + "\n" + percent.ToString() + "% Accuracy";

        }

        public void UpdateRawPoint(int score3)
        {
            rawScore = score3;
            mult_score_Text.text = score.ToString();
            if (totalPossibleNotes == 0)
            {
                percent = 0;
            }
            else
            {
                percent = Convert.ToDouble(rawScore) / Convert.ToDouble(totalPossibleNotes);
            }
            percent = percent * 100;
            percent = Math.Truncate(percent);
            Debug.Log($"PERCENT IS {percent}");
            score_Text.text = rawScore.ToString() + " / " + totalPossibleNotes.ToString() + " = " + percent.ToString() + "%";
            oppEndScreenText.text = "Opponent stats:\n" + "Score " + score.ToString() + "\n" + percent.ToString() + "% Accuracy";

        }

        public void randomDisplay()
        {
            score_Text.text = "POWER UP";
        }

        public double getPercent() {
            return percent;
        }

        public int getOppScore()
        {
            return score;
        }

    }
}