/*
Description: Instantiate the sequence of all songs
Future Work: Read from a JSON file or randomly create encoded lists
*/

/*
Notes:
    no consecutive gestures
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SongData
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

    public SongData(string name, int bpm, List<int> lane, List<int> direction, List<int> rate, List<float> startDelay) 
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

public class Sequence 
{
    // a name of a song mapping it to the song's data
    public Dictionary<string, SongData> sequence { get; set; } = new Dictionary<string, SongData>();

    public Sequence() 
    {
        // for now, hard code
        // "ml7,lr7,mu7,ld7,ll7,rl7,ml7,mr7,lu7,rd7,ll7,rl7,ml7,lr7,ru7,rd7,ml7,ll7" + "###,";
        SongData test = new SongData
        (
            "test",
            125,
            new List<int>() // 0 = finish, 1 = left, 2 = middle, 3 = right
            {
                2, 1, 2, 1, 1, 3, 2, 2, 1 ,3, 1, 3, 2, 1, 3, 3, 2, 1, 0
            },
            new List<int>() // 0 = finish, 1 = up, 2 = down, 3 = left, 4 = right
            {
                3, 4, 1, 2, 3, 3, 3, 4, 1, 2, 3, 3, 3, 4, 1, 2, 3, 3, 0
            },
            new List<int>() // 0 = finish, else
            {
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0
            },
            new List<float>()
            {
                1.67f, 5.68f, 5.58f
            }
        );
        
        sequence.Add
        (
            "test", // name of song
            test
        );
    }

    public List<int> GenerateRandom(int length, int maxValue, int minValue = 1, int seed = 0)
    {
        System.Random random = new System.Random(seed);
        List<int> floatList = new List<int>();
        int prev = 0;

        for (int i = 0; i < length - 1; i++)
        {   
            int randomFloat = 0;
            do
            {
                randomFloat = random.Next(minValue, maxValue + 1);
            } while (randomFloat == prev);

            prev = randomFloat;
            floatList.Add(randomFloat);
        }

        floatList.Add(0);
        Debug.Log($"Random Generation: Length - {length}, MaxValue - {maxValue}, MinValue - {minValue}, List - {floatList}");
        return floatList;
    }
}