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
    public int length { get; set; }
    public int maxLane { get; set; }
    public int maxDirection { get; set; }
    public float beat { get; set; }  
    public string name { get; set; }
    public List<int> lane { get; set; }
    public List<int> direction { get; set; }
    public List<int> rate { get; set; }
    public List<float> startDelay { get; set; }
    public List<float> speedList { get; set; }

    private int bpm_low = 80, bpm_high = 240;

    public Song(string name, int bpm, List<float> startDelay, int length = 40) 
    {
        this.name = name;
        this.bpm = bpm;
        this.startDelay = startDelay;
        this.length = length;
        this.maxLane = 3;
        this.maxDirection = 4;

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

public class Album
{
    public Dictionary<string, Song> songList { get; set; } = new Dictionary<string, Song>();

    public Album() 
    {
        Song test = new Song
        (
            "test",
            125,
            new List<float>()
            {
                1.67f, 5.68f, 5.58f
            },
            50
        );

        songList.Add("test", test);
    }
}