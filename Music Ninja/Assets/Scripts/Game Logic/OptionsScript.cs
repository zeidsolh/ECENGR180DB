using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour
{
    public GameObject MenuMain;
    public GameObject OptionsScreen;
    public static OptionsScript instance;

    private void Awake()
    {
        instance = this;
    }

    public void OptionsButton()
    {
        if(PlayerPrefs.GetInt("MainMenu")==1){     
            MainMenu.instance.Options();
            MenuMain.SetActive(false);
            OptionsScreen.SetActive(true);
            options.instance.setButton();
             PlayerPrefs.SetInt("MainMenu", 0);
        }
    }

    public void BackOptions()
    {
        if(PlayerPrefs.GetInt("MainMenu")==0){  
        OptionsScreen.SetActive(false);
        MenuMain.SetActive(true);
        PlayerPrefs.SetInt("MainMenu", 0);
        }
    }
}
