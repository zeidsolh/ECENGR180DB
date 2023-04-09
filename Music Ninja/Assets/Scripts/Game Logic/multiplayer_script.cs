using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class multiplayer_script : MonoBehaviour


{

    int n;
    public void OnButtonPress()
    {
        n++;
        Debug.Log("Button clicked " + n + " times.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options()
    {
        Debug.Log("Options menu selected.");
        //SceneManager.LoadScene(2);
    }

    public void Multiplayer()
    {
        Debug.Log("Multiplayer menu selected.");
        //SceneManager.LoadScene(2);
    }


    public void Quit()
    {
        Debug.Log("Quitting the game.");
        Application.Quit();
    }



    Scene scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Active Scene name is: " + scene.name + "\nActive Scene index: " + scene.buildIndex);
    }

}
