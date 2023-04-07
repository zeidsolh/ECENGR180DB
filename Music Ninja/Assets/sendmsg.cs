using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class sendmsg : MonoBehaviour
{
    public struct ScoreMessage : NetworkMessage
    {
        public int score;
        public int scorePos;
        public int lives;
    }

    public void SendScore()
    {
        ScoreMessage msg = new ScoreMessage()
        {
            score = 10,
            scorePos = 3,
            lives = 5
        };

        NetworkServer.SendToAll(msg);
    }

    public void SetupClient()
    {
        
        NetworkClient.Connect("localhost");
        NetworkClient.RegisterHandler<ScoreMessage>(OnScore);
    }

    public void OnScore(ScoreMessage msg)
    {
        Debug.Log("OnScoreMessage " + msg.score);
    }
}
