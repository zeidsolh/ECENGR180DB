using System.Collections;
using System.Collections.Generic;
using MirrorBasics;
using UnityEngine;
using UnityEngine.UI;

public class options : MonoBehaviour
{

    [SerializeField] Button easy;
    [SerializeField] Button medium;
    [SerializeField] Button hard;

    [SerializeField] Button song1;
    [SerializeField] Button song2;
    [SerializeField] Button song3;
    [SerializeField] Button song4;
    [SerializeField] Button song5;
    [SerializeField] Button song6;

    public static options instance;




    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("In options");
    }

    private void Awake()
    {
        instance = this;
    }

    public void setButton()
    {
        int level;
        level = PlayerPrefs.GetInt("gameDifficulty", 1);
        int songSelected;
        songSelected = PlayerPrefs.GetInt("songNumber", 1);
        // new...
        //string song = "";


        if (level == 1)
        {
            easyButton();
        }
        else if (level == 2)
        {
            mediumButton();
        }
        else if (level == 3)
        {
            hardButton();
        }

        if (songSelected == 1)
        {
            songOne();
        }
        else if (songSelected == 2)
        {
            songTwo();
        }
        else if (songSelected == 3)
        {
            songThree();
        }
        else if (songSelected == 4)
        {
            songFour();
        }
        else if (songSelected == 5)
        {
            songFive();
        }
        else if (songSelected == 6)
        {
            songSix();
        }

    }

    public void easyButton()
    {
        PlayerPrefs.SetInt("gameDifficulty", 1);
        easy.interactable = false;
        medium.interactable = true;
        hard.interactable = true;
    }

    public void mediumButton()
    {
        PlayerPrefs.SetInt("gameDifficulty", 2);
        easy.interactable = true;
        medium.interactable = false;
        hard.interactable = true;
    }

    public void hardButton()
    {
        PlayerPrefs.SetInt("gameDifficulty", 3);
        easy.interactable = true;
        medium.interactable = true;
        hard.interactable = false;
    }

    public void songOne()
    {
        PlayerPrefs.SetInt("songNumber", 1);
        song1.interactable = false;
        song2.interactable = true;
        song3.interactable = true;
        song4.interactable = true;
        song5.interactable = true;
        song6.interactable = true;
    }

    public void songTwo()
    {
        PlayerPrefs.SetInt("songNumber", 2);
        song1.interactable = true;
        song2.interactable = false;
        song3.interactable = true;
        song4.interactable = true;
        song5.interactable = true;
        song6.interactable = true;
    }

    public void songThree()
    {
        PlayerPrefs.SetInt("songNumber", 3);
        song1.interactable = true;
        song2.interactable = true;
        song3.interactable = false;
        song4.interactable = true;
        song5.interactable = true;
        song6.interactable = true;
    }

    public void songFour()
    {
        PlayerPrefs.SetInt("songNumber", 4);
        song1.interactable = true;
        song2.interactable = true;
        song3.interactable = true;
        song4.interactable = false;
        song5.interactable = true;
        song6.interactable = true;
    }

    public void songFive()
    {
        PlayerPrefs.SetInt("songNumber", 5);
        song1.interactable = true;
        song2.interactable = true;
        song3.interactable = true;
        song4.interactable = true;
        song5.interactable = false;
        song6.interactable = true;
    }

    public void songSix()
    {
        PlayerPrefs.SetInt("songNumber", 6);
        song1.interactable = true;
        song2.interactable = true;
        song3.interactable = true;
        song4.interactable = true;
        song5.interactable = true;
        song6.interactable = false;
    }

}