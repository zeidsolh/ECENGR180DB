using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class songList : MonoBehaviour
{
    // Crab Rave, 125 bpm
    // Matangi,   82 bpm
    // Flight,    87 bpm
    // Overtime,  114 bpm

    public GameObject menuPanel;
    public Text numberText;
    public int selectedNumber = 1;

    private bool isPaused = false;
    private int[] numberOptions = { 1, 2, 3, 4, 5 };

    void Start()
    {
        UpdateNumberText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ResumeGame();
        }
    }

    public void SelectNextNumber()
    {
        int currentIndex = System.Array.IndexOf(numberOptions, selectedNumber);
        if (currentIndex < numberOptions.Length - 1)
        {
            selectedNumber = numberOptions[currentIndex + 1];
        }
        else
        {
            selectedNumber = numberOptions[0];
        }
        UpdateNumberText();
    }

    public void SelectPreviousNumber()
    {
        int currentIndex = System.Array.IndexOf(numberOptions, selectedNumber);
        if (currentIndex > 0)
        {
            selectedNumber = numberOptions[currentIndex - 1];
        }
        else
        {
            selectedNumber = numberOptions[numberOptions.Length - 1];
        }
        UpdateNumberText();
    }

    void UpdateNumberText()
    {
        numberText.text = "Number of players: " + selectedNumber;
    }

    public void StartGame()
    {
        // Replace this with code to start your game with the selected number of players
        Debug.Log("Starting game with " + selectedNumber + " players");
        menuPanel.SetActive(false);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        // Replace this with code to show/hide a pause menu UI if you have one
        Debug.Log(isPaused ? "Game paused" : "Game unpaused");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            TogglePause();
        }
    }
}
