/*
Description: Each song has a Song class object to keep track of it's name, bpm, sequence, highscore etc.
Sequencing is explained below.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Song
{
    public int bpm { get; set; }
    public float beat { get; set; }  
    public string name { get; set; }
    public List<int> lane { get; set; }
    public List<int> direction { get; set; }
    public List<int> rate { get; set; }
    public List<float> startDelay { get; set; }
    public List<float> speedList { get; set; }

    private int bpm_low = 80, bpm_high = 240;

    public Song(string name, int bpm, List<int> lane, List<int> direction, List<int> rate, List<float> startDelay) 
    {
        this.name = name;
        this.bpm = bpm;
        this.lane = lane;
        this.direction = direction;
        this.rate = rate;
        this.startDelay = startDelay;

        beat = (bpm > bpm_low && bpm < bpm_high) ? 60f / bpm : 128f;
        speedList = new List<float>()
        {
            beat / 8.0f, 
            beat / 2f,
            beat,
            beat * 2
        };
    }
}