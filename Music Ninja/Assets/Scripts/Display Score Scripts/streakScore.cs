using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class streakScore : MonoBehaviour
{
    GameObject inputObject;
    getInput inputScript;
    int streak;
    int powerUps;
    int previousStreak;
    public Text streakNumber;
    public Text powerUpNumber;

    void Start()
    {
        inputObject = GameObject.Find("Hitbox Trigger Box");
        inputScript = inputObject.GetComponent<getInput>();
        streak = inputScript.streak;
        powerUps = 0;
        previousStreak = streak;

    }

    // Update is called once per frame
    void Update()
    {
        previousStreak = streak;
        streak = inputScript.streak;
        if (streak % 2 == 0 && streak != 0 && streak != previousStreak)
        {
            powerUps++;

        }
        streakNumber.text = streak.ToString();
        powerUpNumber.text = powerUps.ToString();
    }
}
