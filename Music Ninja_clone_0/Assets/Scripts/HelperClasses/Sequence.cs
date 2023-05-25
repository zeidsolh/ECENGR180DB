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

public class Sequence 
{
    // a name mapping it to a dictionary where it holds the sequence of the song in 3 different vectors -> lane, direction, and timing
    public Dictionary<string, Dictionary<string, List<float>>> sequence { get; set; } = new Dictionary<string, Dictionary<string, List<float>>>();

    public Sequence() 
    {
        // for now, hard code
        // "ml7,lr7,mu7,ld7,ll7,rl7,ml7,mr7,lu7,rd7,ll7,rl7,ml7,lr7,ru7,rd7,ml7,ll7" + "###,";
        sequence.Add
        (
            "test", // name of song
            new Dictionary<string, List<float>>()
            {
                {
                    "lane",
                    new List<float>() // 0 = finish, 1 = left, 2 = middle, 3 = right
                    {
                        2, 1, 2, 1, 1, 3, 2, 2, 1 ,3, 1, 3, 2, 1, 3, 3, 2, 1, 0
                    }
                },
                {
                    "direction",
                    new List<float>() // 0 = finish, 1 = up, 2 = down, 3 = left, 4 = right
                    {
                        3, 4, 1, 2, 3, 3, 3, 4, 1, 2, 3, 3, 3, 4, 1, 2, 3, 3, 0
                    }
                },
                {
                    "rate",
                    new List<float>() // 0 = finish, else
                    {
                        7, 7, 7 ,7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0
                    }
                },
                {
                    "startDelay",
                    new List<float>()
                    {
                        5.68f - 2.3f - 0.45f - 1.9f + 4f - 5.57f - 0.3f + 2f + 0.51f, 5.68f, 5.58f
                        //10.0f - 10.0f
                    }
                },
                {
                    "bpm",
                    new List<float>()
                    {
                        125
                    }
                }
            }
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
                randomFloat = random.Next() * (maxValue - minValue) + minValue;
            } while (randomFloat == prev);

            prev = randomFloat;
            floatList.Add((float)randomFloat);
        }

        floatList.Add(0);
        Debug.Log($"Random Generation: Length - {length}, MaxValue - {maxValue}, MinValue - {minValue}, List - {floatList}");
        return floatList;
    }
}