using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


namespace MirrorBasics
{

    public class Player : NetworkBehaviour
    {

        public static Player localPlayer;
        [SyncVar] public string matchID;
        [SyncVar] public int playerIndex;
        [SyncVar(hook = nameof(ScoreUpdated))] public int playerScore;

        NetworkMatch networkMatchChecker;

        void Start()
        {

            networkMatchChecker = GetComponent<NetworkMatch>();
            if (isLocalPlayer)
            {
                localPlayer = this;
            }
            else
            {
                UILobby.instance.SpawnUIPlayerPrefab(this);
            }
            
        }
        // host game

        public void HostGame()
        {
            string matchID = MatchMaker.GetRandomMatchID();
            CmdHostGame(matchID);
        }

        [Command]
        void CmdHostGame(string _matchID)
        {
            matchID = _matchID;
            if (MatchMaker.instance.HostGame(_matchID, this, out playerIndex))
            {
                Debug.Log($"<color = green>Game Hosted Successfully</color>");
                networkMatchChecker.matchId = _matchID.ToGuid();
                TargetHostGame(true, _matchID, playerIndex);
            }
            else
            {
                Debug.Log($"<color = red>Game Hosted Unsuccessfully</color>");
                TargetHostGame(false, _matchID, playerIndex);
            }
        }

        [TargetRpc]
        void TargetHostGame(bool success, string _matchID, int _playerIndex)
        {
            playerIndex = _playerIndex;
            matchID = _matchID;
            Debug.Log($"MatchID {matchID} == {_matchID}");
            UILobby.instance.HostSuccess(success, _matchID);
        }

        // join game

        public void JoinGame(string _inputID)
        {
            CmdJoinGame(_inputID);
        }

        [Command]
        void CmdJoinGame(string _matchID)
        {
            matchID = _matchID;
            if (MatchMaker.instance.JoinGame(_matchID, this, out playerIndex))
            {
                Debug.Log($"<color = green>Game Joined Successfully</color>");
                networkMatchChecker.matchId = _matchID.ToGuid();
                TargetJoinGame(true, _matchID, playerIndex);
            }
            else
            {
                Debug.Log($"<color = red>Game Joined Unsuccessfully</color>");
                TargetJoinGame(false, _matchID, playerIndex);
            }
        }

        [TargetRpc]
        void TargetJoinGame(bool success, string _matchID, int _playerIndex)
        {
            playerIndex = _playerIndex;
            matchID = _matchID;
            Debug.Log($"MatchID {matchID} == {_matchID}");
            UILobby.instance.JoinSuccess(success, _matchID);
        }


        // begin match

        public void BeginGame()
        {
            CmdBeginGame();
        }

        [Command]
        void CmdBeginGame()
        {
            MatchMaker.instance.BeginGame(matchID);
            Debug.Log($"<color = red>Game Beginning</color>");
        }

        public void StartGame()
        {
            TargetBeginGame();
        }

        [TargetRpc]
        void TargetBeginGame()
        {
            Debug.Log($"MatchID {matchID} | Beginning");
            //Additionally load game scene
            UILobby.instance.DisableConnect();
            SceneManager.LoadScene("MultiplayerGame", LoadSceneMode.Additive);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        }


        // update score to server
        [Client]
        public void UpdateScore(int score)
        {
            if (!hasAuthority) return;

            playerScore = score;
            CmdUpdateScore(score);
        }

        [Command]
        void CmdUpdateScore(int score)
        {
            playerScore = score;
        }






        // update score to clients
        //[SyncVar(hook = nameof(ScoreUpdated))] int score;

        void ScoreUpdated(int oldScore, int newScore)
        {
            Debug.Log($"NEW SCORE {newScore}");
            score_display_v2.instance.UpdatePoint(newScore);
        }

        [Server]
        void ScoreUpdated(int score)
        {
            RpcScoreUpdated(score);
        }

        [ClientRpc]
        void RpcScoreUpdated(int score)
        {
            score_display_v2.instance.UpdatePoint(score);
        }

    }

}
