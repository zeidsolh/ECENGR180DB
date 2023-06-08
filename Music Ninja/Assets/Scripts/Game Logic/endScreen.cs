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
        public GameObject menuSong;
        getInput inputScript;
        int score;
        int rawScore;
        public TMP_Text scoreText;
        public int totalPossibleNotes;
        Button continueButton;
        public static endScreen instance;
        double oppPercent = 0;
        int oppScore = 0;
        public TMP_Text highScoreText;
        public TMP_Text WonLost;
        public TMP_Text songName;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            menuSong.SetActive(true);
            inputObject = GameObject.Find("Hitbox Trigger Box");
            inputScript = inputObject.GetComponent<getInput>();
            score = inputScript.playerScore;
            rawScore = inputScript.accuracy;
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            highScoreText.text = "High Score = " + PlayerPrefs.GetInt("HighScore", 0).ToString();
            int songSelected;
            songSelected = PlayerPrefs.GetInt("songNumber", 1);
            if (songSelected == 1)
            {
                songName.text = "Song Played: Crab Rave";
            }
            else if (songSelected == 2)
            {
                songName.text = "Song Played: Disconnected";
            }
            else if (songSelected == 3)
            {
                songName.text = "Song Played: Flight";
            }
            else if (songSelected == 4)
            {
                songName.text = "Song Played: Every Language is Alive";
            }
            else if (songSelected == 5)
            {
                songName.text = "Song Played: Unity";
            }
            else if (songSelected == 6)
            {
                songName.text = "Song Played: Breathing Under Water";
            }
        }

        void Update()
        {
            totalPossibleNotes = inputScript.possiblePoints;
            score = inputScript.playerScore;
            rawScore = inputScript.accuracy;
            double percent = 0;
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
            scoreText.text = "Player Name\n" + "Score " + score.ToString() + "\n" + percent.ToString() + "% Accuracy";
            oppPercent = score_display_v2.instance.getPercent();
            oppScore = score_display_v2.instance.getOppScore();
            if (oppScore > score)
            {
                WonLost.text = "You Lost :(";
            }
            else if(oppScore < score)
            {
                WonLost.text = "You Won :)";
            }
            else
            {
                WonLost.text = "You drew :)";
            }
            if (PlayerPrefs.GetInt("Online", 0) == 0)
            {
                WonLost.text = "Good Game!";
            }

        }

        public void ContinueFunction()
        {
            PlayerPrefs.SetInt("MainMenu", 1);
            menuSong.SetActive(false);
            if (PlayerPrefs.GetInt("Online", 0) == 0)
            {
                MainMenu.instance.DestroyNetworkManager();
                SceneManager.LoadScene("MainMenu");
            }
            else if(PlayerPrefs.GetInt("Online") == 1)
            {
                Player.localPlayer.clientDisconnect();
            }
            
            
        }

    }
}
