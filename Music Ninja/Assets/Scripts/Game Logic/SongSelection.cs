using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class SongSelection : MonoBehaviour
{
    public GameObject song1;
    public GameObject song2;
    public GameObject song3;
    public GameObject song4;
    public GameObject song5;
    public GameObject song6;


    // Start is called before the first frame update
    void Start()
    {
        // get song selected
        int level;
        level = PlayerPrefs.GetInt("gameDifficulty", 1);
        int songSelected;
        songSelected = PlayerPrefs.GetInt("songNumber", 1);

        // set all songs to false by default
        song1.SetActive(false);
        song2.SetActive(false);
        song3.SetActive(false);
        song4.SetActive(false);
        song5.SetActive(false);
        song6.SetActive(false);


        // set song selected
        switch (songSelected)
        {
            case 1:
                {
                    song1.SetActive(true);
                    break;
                }
            case 2:
                {
                    song2.SetActive(true);
                    break;
                }
            case 3:
                {
                    song3.SetActive(true);
                    break;
                }
            case 4:
                {
                    song4.SetActive(true);
                    break;
                }
            case 5:
                {
                    Debug.Log("Unity -TheFatRat");
                    song5.SetActive(true);
                    break;
                }
            case 6:
                {
                    song6.SetActive(true);
                    break;
                }
        }

        /* if (songSelected == 1)
         {
             song1.SetActive(true);
             song2.SetActive(false);
         }
         else if (songSelected == 2)
         {
             song1.SetActive(false);
             song2.SetActive(true);
         } else if (songSelected == 3)
         {

         }*/
    }
}