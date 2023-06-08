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


        curr.lane = GenerateRandom(curr.length, new HashSet<int>() { }, curr.maxLane);
        curr.direction = GenerateRandom(curr.length, new HashSet<int>() { }, curr.maxDirection, 1, 0, true);

        if (PlayerPrefs.GetInt("gameDifficulty", 1) == 1)
        {
            // easy
            curr.rate = GenerateRandom(curr.length, new HashSet<int>() { 5 }, 8, 7);
        } else if (PlayerPrefs.GetInt("gameDifficulty",1) == 2)
        {
            // medium and hard
            curr.rate = GenerateRandom(curr.length, new HashSet<int>() { 5 }, 9, 6);
        } else if (PlayerPrefs.GetInt("gameDifficulty",1) == 3)
        {
            // hard
            curr.rate = GenerateRandom(curr.length, new HashSet<int>() { 5 }, 7, 6);
        }
        
        

        return curr;
    }

    // generate random sequences
    // ranges are inclusive []
    public List<int> GenerateRandom(int length, HashSet<int> exclude, int maxValue, int minValue = 1, int seed = 0, bool direction = false)
    {
        System.Random random = new System.Random(seed);
        List<int> output = new List<int>();
        int prev = 0;

        for (int i = 0; i < length - 1; i++)
            {   
                int randomNum = 0;
                do
                {
                    randomNum = random.Next(minValue, maxValue + 1);
                } while ((randomNum == prev)&&direction || exclude.Contains(randomNum));

                

                prev = randomNum;
                output.Add(randomNum);
            }

        output.Add(0);
        // for (int i = 0; i < output.Count; i++)
        //     Debug.Log($"{output[i]}");
        return output;
    }
}