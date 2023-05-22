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
    public string name { get; set; }
    public List<float> lane { get; set; }
    public List<float> direction { get; set; }
    public List<float> rate { get; set; }
    public List<float> startDelay { get; set; }

    public SongData(string name, int bpm, List<float> lane, List<float> direction, List<float> rate, List<float> startDelay) 
    {
        this.name = name;
        this.bpm = bpm;
        this.lane = lane;
        this.direction = direction;
        this.rate = rate;
        this.startDelay = startDelay;
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
            new List<float>() // 0 = finish, 1 = left, 2 = middle, 3 = right
            {
                2, 1, 2, 1, 1, 3, 2, 2, 1 ,3, 1, 3, 2, 1, 3, 3, 2, 1, 0
            },
            new List<float>() // 0 = finish, 1 = up, 2 = down, 3 = left, 4 = right
            {
                3, 4, 1, 2, 3, 3, 3, 4, 1, 2, 3, 3, 3, 4, 1, 2, 3, 3, 0
            },
            new List<float>() // 0 = finish, else
            {
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0
            },
            new List<float>()
            {
                5.68f - 2.3f - 0.45f - 1.9f + 4f - 5.57f - 0.3f + 2f + 0.51f, 5.68f, 5.58f
            }
        );
        
        sequence.Add
        (
            "test", // name of song
            test
        );
    }

    public List<float> GenerateRandom(int length, int maxValue, int minValue = 1, int seed = 0)
    {
        System.Random random = new System.Random(seed);
        List<float> floatList = new List<float>();
        int prev = 0;

        for (int i = 0; i < length - 1; i++)
        {   
            int randomFloat = 0;
            do
            {
                randomFloat = random.Next(minValue, maxValue+1);
            } while (randomFloat == prev);

            prev = randomFloat;
            floatList.Add((float)randomFloat);
        }

        floatList.Add(0);
        Debug.Log($"Random Generation: Length - {length}, MaxValue - {maxValue}, MinValue - {minValue}, List - {floatList}");
        return floatList;
    }
}