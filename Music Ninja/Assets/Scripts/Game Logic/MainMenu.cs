/*
Description:
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour


{
    [SerializeField] GameObject MainMenuCanvas;
    public static MainMenu instance;
    public GameObject NetworkManagerGameObject;
    public GameObject MenuSong;

    private void Awake()
    {
        instance = this;
        PlayerPrefs.SetInt("MainMenu", 1);
    }

    public void DestroyNetworkManager()
    {
        Destroy(NetworkManagerGameObject);
    }

    int n;
    public void OnButtonPress()
    {
         if(PlayerPrefs.GetInt("MainMenu")==1){     
        n++;
        Debug.Log("Button clicked " + n + " times.");
        PlayerPrefs.SetInt("Online", 0);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        MainMenuCanvas.SetActive(false);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        MenuSong.SetActive(false);
        PlayerPrefs.SetInt("MainMenu", 0);
         }
    }

    public void Options()
    {
        Debug.Log("Options menu selected.");
        //PlayerPrefs.SetInt("gameDifficulty", 2);
        //PlayerPrefs.SetInt("songNumber", 2);
        //Debug.Log(PlayerPrefs.GetInt("songNumber"));
        //SceneManager.LoadScene(2);
    }

    public void Multiplayer()
    {
        if(PlayerPrefs.GetInt("MainMenu")==1){   
        Debug.Log("Multiplayer menu selected.");
        PlayerPrefs.SetInt("Online", 1);
        //SceneManager.LoadScene(2);
        PlayerPrefs.SetInt("MainMenu", 0);
         }
    }


    public void Quit()
    {
        Debug.Log("Quitting the game.");
        NetworkManager.Singleton.Shutdown();
        Application.Quit();
    }



    Scene scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Active Scene name is: " + scene.name + "\nActive Scene index: " + scene.buildIndex);
    }

}
