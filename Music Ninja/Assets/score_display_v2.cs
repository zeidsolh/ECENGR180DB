using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class score_display_v2 : MonoBehaviour
{
    public static score_display_v2 instance;

    int score = 0;
    public Text score_Text;

    private void Awake()
    {

        instance = this;
    }

    void Start()
    {
        score_Text.text = score.ToString();
    }

    void Update()
    {
    }

    public void UpdatePoint(int score1)
    {
        score_Text.text = score1.ToString();
    }


}