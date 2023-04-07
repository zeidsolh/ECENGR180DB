using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


namespace MirrorBasics
{

    public class TurnManager : NetworkBehaviour
    {

        List<Player> players = new List<Player>();

        public void AddPlayer(Player _player) {
            players.Add(_player);
        }

        void Update()
        {
            for(int i = 0; i < players.Count; i++)
            {
                
                Debug.Log($"Player {i} Score {players[i].playerScore}");
            }
        }
    }


}
