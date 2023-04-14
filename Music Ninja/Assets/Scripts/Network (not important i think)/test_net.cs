/*
Description:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


namespace MirrorBasics
{


    public class test_net : NetworkBehaviour
    {
        int score;
        int prev_score;
        public int opponent_score;
        getInput inputScript1;
        GameObject inputObject1;

        void onScoreChange(int oldCount, int newCount)
        {
            Debug.Log($"Server Score: {newCount}");
            if (!isServer)
            {
                score_display_v2.instance.UpdatePoint(newCount);
            }

        }


        void Start()
        {
            inputObject1 = GameObject.Find("Hitbox Trigger Box");
            Debug.Log("STARTING NWOWW");
            Debug.Log(inputObject1);
            inputScript1 = inputObject1.GetComponent<getInput>();
            score = inputScript1.playerScore;
            prev_score = score;
            opponent_score = 0;
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log("PLAYERRRRRRR");
            //Debug.Log(Player.localPlayer);
            inputObject1 = GameObject.Find("Hitbox Trigger Box");
            inputScript1 = inputObject1.GetComponent<getInput>();
            prev_score = score;
            score = inputScript1.playerScore;
            //Debug.Log(isLocalPlayer);
            //Debug.Log(isServer);


            if (isServer && score > prev_score)
            {
                Debug.Log("Sendingg............................");
                Debug.Log($"My score is {score}");
                receivedByClient();
                hola();

            }
            else if (score > prev_score)
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
            score_display_v2.instance.UpdatePoint(opponent_score);
        }

        [ClientRpc]
        void receivedByClient()
        {
            server_score++;
            Debug.Log($"Server Score {score}");
        }

        [SyncVar(hook = nameof(onScoreChange))]
        int server_score = 0;

    }

}
