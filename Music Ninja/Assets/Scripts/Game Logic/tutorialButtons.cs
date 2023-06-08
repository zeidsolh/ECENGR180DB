using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialButtons : MonoBehaviour
{
    public GameObject MenuMain;
    public GameObject TutorialScreen;
    public GameObject VideObject;
    public GameObject hitboxTrigger;
    public GameObject songMenu;
    public static tutorialButtons instance;

    private void Awake()
    {
        instance = this;
    }

    public void TutorialButton()
    {
         if(PlayerPrefs.GetInt("MainMenu")==1){     
        MenuMain.SetActive(false);
        TutorialScreen.SetActive(true);
        VideObject.SetActive(true);
        hitboxTrigger.SetActive(false);
        songMenu.SetActive(false);
         PlayerPrefs.SetInt("MainMenu", 0);
        }
    }

    public void BackTutorial()
    {
        if(PlayerPrefs.GetInt("MainMenu")==0){ 
        MenuMain.SetActive(true);
        TutorialScreen.SetActive(false);
        VideObject.SetActive(false);
        hitboxTrigger.SetActive(true);
        songMenu.SetActive(true);
            PlayerPrefs.SetInt("MainMenu", 1);
    }}
}
