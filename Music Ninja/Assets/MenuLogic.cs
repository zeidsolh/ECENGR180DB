using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MenuLogicNamespace
{
    public class MenuLogic : MonoBehaviour
    {

        public MenuLogic()
        {
            songChoice = "";
            difficulty = "";
        }

        
        //public GameObject spriteObj;
        public Sprite songListSprite;
        public bool inMenuScreen = true;
        void Start()
        {
            songListSprite = GetComponent<SpriteRenderer>().sprite; // = songListSprite;
            if (songListSprite == null ) {
                Debug.Log("songList SpriteRenderer not found.");
            }
            //spriteObj = GameObject.FindGameObjectWithTag("songList");
        }
        //public SpriteRenderer songListSprite;
        
        bool showSongList = true;
        //string songChoice = "";
        public void init()
        {
            //if (spriteObj != null)
            //{
                //songListSprite = spriteObj.GetComponent<SpriteRenderer>();
            //}
            if (showSongList)
            {
                /*
                if (songListSprite == null)
                {
                    //songListSprite = GetComponent<SpriteRenderer>();
                    //songListSprite = transform.GetComponentInChildren<SpriteRenderer>();
                    if (songListSprite == null)
                    {
                        Debug.LogError("songListSprite is not assigned and cannot be found.");
                        return;
                    }
                }
                */


                //songListSprite.enabled = true;
                Debug.Log("Press 1/2/3/4 to select your song.");
            }
            while (showSongList)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))   // use this as a temporary substitute for speech
                {
                    // Use setSong(...) method later on
                    Debug.Log("You chose Crab Rave.");
                    songChoice = "Crab Rave";
                    showSongList = false;
                    //songListSprite.enabled = false;
                }
            }

            Debug.Log("Choose your difficulty.");
            // Get the player's difficulty choice and set difficulty to it
            
        }
        public void PausePlay()
        {
            // Check for user speech commands:
            // on_message("MusicNinja"..."
            // string msg = message.payload(2:-1);

            Debug.Log("Start?");

            string msg = "start";   // for now;
            msg.ToLower();
            string confirm = "";

            // Move below code to Menu or UI 
            if (msg == "quit")
            {
                // pause the current game
                // display("Are you sure?);
                // display("Yes/No");
                // get user speech
                confirm = "yes"; // for now
                confirm.ToLower();
                if (confirm == "yes")
                {
                    // Go to the Menu screen
                }
                else if (confirm == "no")
                {
                    // resume
                }
            }
            else if (msg == "pause")
            {
                // pause the current game
                Time.timeScale = 0;
                Debug.Log("Set timescale to 0.");
                // display("Resume?")
                // display("Yes/No?)
                // get user speech
                confirm = "yes"; // for now
            }
            if (confirm == "yes")
            {
                // resume the game
                Time.timeScale = 1;
                //Debug.Log("Set timescale to 1.");
            }
            else if (confirm == "no")
            {
                // quit the game
            }
            else if (msg == "start")
            {

            }
        }
        public void displaySongList()
        {
            // write to on-screen display
            Debug.Log("Displaying Song List."); // for now
        }
        public void setSongChoice()
        {
            // get user speech
            songChoice = "Crab Rave";   // for now
        }
        public void setDifficulty()
        {
            // get user difficulty from speech recognition
            difficulty = "easy";   // for now
        }
        public int updateHighScores(Song song, int score)
        {
            return 0;
            // Write to local game file that stores an array of 10 x number of songs in song list
            // each song records the top 10 scores in a .txt file whenever a game is finished
        }
        private Song[] songList;
        //private List<Song> songList;
        public string songChoice;
        public string difficulty;
    }
}

