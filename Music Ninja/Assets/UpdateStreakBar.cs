using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdateStreakBar : MonoBehaviour
{

    public Image streakBarImage;
    GameObject inputObject;
    getInput inputScript;
    public int streak=0;

    // Start is called before the first frame update
    void Start()
    {
        inputObject = GameObject.Find("Hitbox Trigger Box");
        inputScript = inputObject.GetComponent<getInput>();
        streak = inputScript.streak;
    }

    // Update is called once per frame
    void Update()
    {
        streak = inputScript.streak;
        streak= streak%2;
        //Debug.Log(streak);
        float fill =(float)streak/2f;
        Debug.Log(fill);
        streakBarImage.fillAmount = fill;
        //streakBarImage.fillAmount = 0.5f;
    }
}
