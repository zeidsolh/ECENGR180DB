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
        MenuMain.SetActive(false);
        TutorialScreen.SetActive(true);
        VideObject.SetActive(true);
        hitboxTrigger.SetActive(false);
        songMenu.SetActive(false);
    }

    public void BackTutorial()
    {
        MenuMain.SetActive(true);
        TutorialScreen.SetActive(false);
        VideObject.SetActive(false);
        hitboxTrigger.SetActive(true);
        songMenu.SetActive(true);
    }
}
