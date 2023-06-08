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
        MainMenu.instance.Options();
        MenuMain.SetActive(false);
        OptionsScreen.SetActive(true);
        options.instance.setButton();
    }

    public void BackOptions()
    {
        OptionsScreen.SetActive(false);
        MenuMain.SetActive(true);
    }
}
