/*
Description: Instantiate the sequence of all songs
Future Work: Read from a JSON file
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sequence 
{
    // a name mapping it to a dictionary where it holds the sequence of the song in 3 different vectors -> lane, direction, and timing
    public Dictionary<string, Dictionary<string, List<int>>> sequence { get; set; } = new Dictionary<string, Dictionary<string, List<int>>>();

    public Sequence() 
    {
        // for now, hard code
        // "ml7,lr7,mu7,ld7,ll7,rl7,ml7,mr7,lu7,rd7,ll7,rl7,ml7,lr7,ru7,rd7,ml7,ll7" + "###,";
        sequence.Add
        (
            "test",
            new Dictionary<string, List<int>>()
            {
                {
                    "lane",
                    new List<int>() // 0 = finish, 1 = left, 2 = middle, 3 = right
                    {
                        2, 1, 2, 1, 1, 3, 2, 2, 1 ,3, 1, 3, 2, 1, 3, 3, 2, 1, 0
                    }
                },
                {
                    "direction",
                    new List<int>() // 0 = finish, 1 = up, 2 = down, 3 = left, 4 = right
                    {
                        3, 4, 1, 2, 3, 3, 3, 4, 1, 2, 3, 3, 3, 4, 1, 2, 3, 3, 0
                    }
                },
                {
                    "rate",
                    new List<int>() // 0 = finish, else
                    {
                        7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0
                    }
                }
            }
        );
    }
}