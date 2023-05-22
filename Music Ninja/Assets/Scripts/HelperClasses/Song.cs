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
    private string m_name; // song name
    public int bpm;
    private float initialSpawnerDelay;
    //private int highscore;
    private SongData seqKey;  // encoded data for spawner
    public float startDelay;    // Used to align the first target with the correct beat
    int m_difficulty;
    public static int highscore;
    public static string players_name;
    public static string opponents_name;
    public static int opponents_score;
    public int winners_score;

    public Song(string nm, int rate, float delay)
    {
        m_name = nm;
        bpm = rate;
        initialSpawnerDelay = delay;
        int winners_score = 0;  //for now
        if (winners_score >= highscore)
        {
            highscore = winners_score;
        }

    }

    public Song(string nm, int rate)
    {
        m_name = nm;
        bpm = rate;
        initialSpawnerDelay = 0.0f;
    }


    public void loadScript(ref Song curSong)
    {
        //public getInput script;
        // Load song script into targets container and the time between notes into the timeBetweenEachNote container
        if (curSong.name() == "Crab Rave")
        {
            //startDelay = 0.2f;
            if (curSong.getDifficulty() == 1)
            {
                // CURRENT DIFFICULTY
                startDelay = 5.68f - 2.3f - 0.45f - 1.9f + 4f - 5.57f - 0.3f + 2f + 0.51f;

                Debug.Log("Diifculty " + curSong.getDifficulty() + " Start Delay " + startDelay);
            }
            else if (curSong.getDifficulty() == 2)
            {
                startDelay = 5.68f;
            }
            else
            {
                startDelay = 5.58f;  // for debugging
            }
            //startDelay = 5.68f; // 5.67f - 4.13f + 5.36f + 5.25f; // - 2.27f;
            // Hard-code the sequence for Crab Rave here or read with infile

            /* 
                The string sequence for a song tells Spawner.cs how to spawn each target for that song.
                Each target has a 3 character string associated with it followed by a comma.
                The first character in each set of 3 corresponds to the lane or initial x position of that target.
                The second character corresponds to the swipe direction for that target.
                The third character corresponds to when that target ought to spawn.
             */
            // char1 = position r/m/l = right-lane, middle-lane, left-lane
            // char2 = rotation u/d/l/r = swipe up/down/left/right
            // char3 = time (ms) between last target spawn and current target spawn
            /*
                Here is how the decoder determines the interval between notes based on the 3rd character:
            //             new implementation       old implementation
            //     0 time: 5........................0
            //single note: 6........................1
            //  two notes: 7       
            //three notes: 8
            // four notes: 9........................5
            //   1/2 note: 1........................2
            //   1/4 note: 2........................3
            // 1/8th note: 3........................4
            // 1/16th note:4   
            //    triplet: 0
            */


            // 1 + 2 + 3 + 4 + 
            // for obstacle : lo_ or ro_
            //"ll6,rr6,ll6,rr6," + //ll6,rr6" + "ll6,rr6," + //ll6,rr6,ll6,rr6" +
            // string sequenceKey = "rd1,ld1,rd2,ru2,rd1," + "ld1,rd1,ld2,lu2,ld1," + "rr1,ll1,rd2,ru2,rd1," + "ll1,rr1,lld2,lu2,ld1," + 
            //     "ll1,mr1,ll2,lr2,ll1," + "rr1,ml1,rr2,rl2,rr1," + "ll1,rr1,ll2,lr2,ll1," + "rr1,ll1,rr2,rl2,rr1," +
            //     "rd1,ld1,lu2,ld2,lu1," + "ld1,rd1,ru2,rd2,ru1," + "ru1,lu1,ld2,lu2,ld1," + "lu1,ru1,rd2,ru2,ru1," + 
            //     "md1,mu1,md2,mu2,md1," + "ld1,lu1,ld2,lu2,ld1," + "rd1,ru1,rd2,ru2,rd1," +
            //     "md1,mu1,md2,mu2,md1," + "ld1,lu1,ld2,lu2,ld1," + "rd1,ru1,rd2,ru2,rd1," +
            //     "mu1,md1,md2,mu2,md1," + "lu1,ld1,ld2,lu2,ld1," +
            //     "ml1,rr2,ml3,rr2,ml2,ll2," + "mr1,ll2,mr3,ll2,mr2,rl2" + "mr1,ll2,mr3,ll2,mr2,rr2," + "ml1,rr2,ml3,rr2,ml2,lr2" +
            //     "ml1,rr2,ml3,rr2,ml2,ll2," + "mr1,ll2,mr3,ll2,mr2,rl2" + "mr1,ll2,mr3,ll2,mr2,rr2," + "ml1,rr2,ml3,rr2,ml2,lr2" +
            //     "ml1,rr2,ml3,rr2,ml2,ll2," + "mr1,ll2,mr3,ll2,mr2,rl2" + "mr1,ll2,mr3,ll2,mr2,rr2," + "ml1,rr2,ml3,rr2,ml2,lr2" +
            //     "ml1,rr2,ml3,rr2,ml2,ll2," + "mr1,ll2,mr3,ll2,mr2,rl2" + "mr1,ll2,mr3,ll2,mr2,rr2," + "ml1,rr2,ml3,rr2,ml2,lr2" +
            //     "ml1,rr2,ml3,rr2,ml2,ll2," + "mr1,ll2,mr3,ll2,mr2,rl2" + "mr1,ll2,mr3,ll2,mr2,rr2," + "ml1,rr2,ml3,rr2,ml2,lr2" + "ml1,rr2,ml3,rr2,ml2,ll7," +
            //     "md7,md7,md7,md7," + "md7,md7,md7,md7," +
            //     "rd1,ld1,rd2,ru2,rd1," + "ld1,rd1,ld2,lu2,ld1," + "rr1,ll1,rd2,ru2,rd1," + "ll1,rr1,lld2,lu2,ld1," +
            //     "ll1,mr1,ll2,lr2,ll1," + "rr1,ml1,rr2,rl2,rr1," + "ll1,rr1,ll2,lr2,ll1," + "rr1,ll1,rr2,rl2,rr1," +
            //     "ml1,rr2,ml3,rr2,ml2,ll2," + "mr1,ll2,mr3,ll2,mr2,rl2" + "ml1,rr2,ml3,rr2,ml2,ll2," + "mr1,ll2,mr3,ll2,mr2,rl2" +
            //     "mr1,ll2,mr3,ll2,mr2,rr2," + "ml1,rr2,ml3,rr2,ml2,lr2" + "mr1,ll2,mr3,ll2,mr2,rr2," + "ml1,rr2,ml3,rr2,ml2,lr2" +
            //     "ml1,rr2,ml3,rr2,ml2,ll2," + "mr1,ll2,mr3,ll2,mr2,rl2" + "ml1,rr2,ml3,rr2,ml2,ll2," + "mr1,ll2,mr3,ll2,mr2,rl2" +
            //     "mr1,ll2,mr3,ll2,mr2,rr2," + "ml1,rr2,ml3,rr2,ml2,lr2" + "mr1,ll2,mr3,ll2,mr2,rr2," + "ml1,rr2,ml3,rr2,ml2,lr2" +
            //     "ml1,rr2,ml3,rr2,ml2,ll2," + "mr1,ll2,mr3,ll2,mr2,rl2" + "ml1,rr2,ml3,rr2,ml2,ll2," + "mr1,ll2,mr3,ll2,mr2,rl2" +
            //     "mr1,ll2,mr3,ll2,mr2,rr2," + "ml1,rr2,ml3,rr2,ml2,lr2" + "rd2,mu2,ll2,rr2,rd2,ru3,ll2,rr2," + "ld2,mu2,rr2,ll2,ld2,lu3,rr2,ll2," +
            //     "md7,md7,md7,md7," +
            //     "###,";
            //test sequence
            //string sequenceKey = "ml7,lr7,mu7,ld7,ll7,rl7,ml7,mr7,lu7,rd7,ll7,rl7,ml7,lr7,ru7,rd7,ml7,ll7,ml7,lr7,mu7,ld7,ll7,rl7,ml7,mr7,lu7,rd7,ll7,rl7,ml7,lr7,ru7,rd7,ml7,ll7" + "###,";
            Sequence seq = new Sequence();
            curSong.setKey(seq.sequence["test"]);
        }
        // else if (curSong.name() == "Matangi")
        // {
        //     startDelay = 8f;
        //     string sequenceKey = "ll5,rr9,ll9,rr9,mu9,md9,mu9,md9,ll9,mr3,rr3,rl4,rr4,ml3,ll3,lr4,ll4,lr4,mr4" + "###,";
        //     curSong.setKey(sequenceKey);
        // }
        // else if (curSong.name() == "Other Song")
        // {
        //     string sequenceKey = "mu0,md3,mu3,md3";
        //     curSong.setKey(sequenceKey);
        // }
    }
    public void setKey(SongData seq)
    {
        seqKey = seq;
    }
    public SongData getKey()
    {
        return seqKey;
    }
    public float getDelay()
    {
        return startDelay;
    }
    public float beat()
    {
        int i = 240;
        int ll = 80;
        if ((ll < bpm) && (bpm < i))
        {
            return (60f / bpm);
        }
        else { return 128.0f; }
    }
    public string name() { return m_name; }
    public void setDifficulty(int d)
    {
        m_difficulty = d;
    }
    public int getDifficulty()
    {
        return m_difficulty;
    }
}