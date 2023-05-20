using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class SongSelection : MonoBehaviour
{
    public GameObject song1;
    public GameObject song2;
    public GameObject song3;


    // Start is called before the first frame update
    void Start()
    {
        // get song selected
        int level;
        level = PlayerPrefs.GetInt("gameDifficulty", 1);
        int songSelected;
        songSelected = PlayerPrefs.GetInt("songNumber", 1);

        if (songSelected == 1)
        {
            song1.SetActive(true);
            song2.SetActive(false);
            song3.SetActive(false);
        }
        else if (songSelected == 2)
        {
            song1.SetActive(false);
            song2.SetActive(true);
            song3.SetActive(false);
        }
        else if (songSelected == 3)
        {
            song1.SetActive(false);
            song2.SetActive(false);
            song3.SetActive(true);
        }
    }
}