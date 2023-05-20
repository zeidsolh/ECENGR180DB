using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class sendingmsg : NetworkBehaviour
{
    int score;
    int prev_score;
    public int opponent_score;
    getInput inputScript;
    GameObject inputObject;

    void onScoreChange(int oldCount, int newCount)
    {
        Debug.Log($"Server Score: {newCount}");
        if (!isServer)
        {
            //score_display_v2.instance.UpdatePoint(newCount);
        }
        
    }


    void Start()
    {
        inputObject = GameObject.Find("Hitbox Trigger Box");
        inputScript = inputObject.GetComponent<getInput>();
        score = inputScript.playerScore;
        prev_score = score;
        opponent_score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        prev_score = score;
        score = inputScript.playerScore;

        if (isServer && score > prev_score)
        {
            Debug.Log($"My score is {score}");
            receivedByClient();
        }
        else if (isLocalPlayer && score>prev_score)
        {
            Debug.Log($"My score is {score}");
            hola();
        }
        
    }



    [Command]
    void hola()
    {
        opponent_score++;
        Debug.Log($"opponent score {opponent_score}");
        //score_display_v2.instance.UpdatePoint(opponent_score);
    }

    [ClientRpc(includeOwner = false)]
    void receivedByClient()
    {
        server_score++;
        //Debug.Log($"Server Score {score}");
    }

    [SyncVar(hook = nameof(onScoreChange))]
    int server_score = 0;

}
