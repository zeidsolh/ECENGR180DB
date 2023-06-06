/*
Description: Instantiate the sequence of all songs
Future Work: Read from a JSON file or randomly create encoded lists
*/

/*
Notes:
    no consecutive gestures
*/

using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sequence 
{
    private Album album = new Album();

    public Song getSong(string name) {
        Song curr = album.songList[name];
        
        
        curr.lane = GenerateRandom(curr.length, curr.maxLane);
        curr.direction = GenerateRandom(curr.length, curr.maxDirection);
        curr.rate = Enumerable.Repeat(7, curr.length).ToList();

        return curr;
    }

    // generate random sequences
    // ranges are inclusive []
    public List<int> GenerateRandom(int length, int maxValue, int minValue = 1, int seed = 0)
    {
        System.Random random = new System.Random(seed);
        List<int> output = new List<int>();
        int prev = 0;

        for (int i = 0; i < length - 1; i++)
        {   
            int randomFloat = 0;
            do
            {
                randomFloat = random.Next(minValue, maxValue + 1);
            } while (randomFloat == prev);

            prev = randomFloat;
            output.Add(randomFloat);
        }

        output.Add(0);
        return output;
    }
}