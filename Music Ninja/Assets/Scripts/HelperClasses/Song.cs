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
            beat / 12.0f,
            beat / 8.0f,
            beat / 2.0f,
            beat
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
                8.66f, 1.28f, 5.87f
            },
            50
        );  // was 1.67f, 9.47 then 5.68f, 5.58f

        Song Disconnected = new Song("Disconnected", 128, new List<float>() { 13.53f, 6.88f, 11.13f }, 242);
        Song Flight = new Song("Flight", 87, new List<float>() { 12.11f, 6.74f, 20.6f }, 219);   // 219 for full song. 47 for intro
        Song EveryLanguage = new Song("Every Language is Alive", 129, new List<float>() { 0.1f, 0.1f, 0.1f }, 358);
        Song Unity = new Song("Unity", 105, new List<float>() { 0.1f, 2.9f, 6.323f }, 249);
        Song BreathingUnderwater = new Song("Breathing Underwater", 140, new List<float>() { 9.9f, 0.1f, 0.1f }, 278);

        songList.Add("test", test);
        songList.Add("Disconnected", Disconnected);
        songList.Add("Flight", Flight);
        songList.Add("Every Language is Alive", EveryLanguage);
        songList.Add("Unity", Unity);
        songList.Add("Breathing Underwater", BreathingUnderwater);
    }
}