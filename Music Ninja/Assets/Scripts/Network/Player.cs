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
        [SyncVar(hook = nameof(possibleScoreUpdated))] public int possibleScore;
        [SyncVar(hook = nameof(RawScoreUpdated))] public int playerRawScore;

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

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.X)){
                sendObstacle("left");
            }
            if (Input.GetKeyDown(KeyCode.O)){
                sendObstacle("right");
            }
            
        }

        public void sendObstacle(string direction)
        {
            if (isLocalPlayer)
            {
                if (streakScore.instance.positivePowerUps())
                {
                    Player[] players = GameObject.FindObjectsOfType<Player>();
                    foreach (Player player in players)
                    {
                        if (player.GetComponent<Player>() != localPlayer)
                        {
                            SendMessageTo(player.gameObject, "left");
                            SendMessageTo(player.gameObject, "right");
                            streakScore.instance.decrementPowerUps();
                        }
                    }
                }
            }
        }

        public void clientDisconnect()
        {
            NetworkClient.Disconnect();
        }

        // host game

        public void HostGame()
        {
            string matchID = MatchMaker.GetRandomMatchID();
            int song;
            song = PlayerPrefs.GetInt("songNumber");
            int difficulty;
            difficulty = PlayerPrefs.GetInt("gameDifficulty");
            CmdHostGame(matchID, song, difficulty);
        }

        [Command]
        void CmdHostGame(string _matchID, int _song, int _difficulty)
        {
            matchID = _matchID;
            if (MatchMaker.instance.HostGame(_matchID, this, out playerIndex, _song, _difficulty))
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
                int song;
                song = MatchMaker.instance.getSongNumber(_matchID);
                int difficulty;
                difficulty = MatchMaker.instance.getGameDifficulty(_matchID);
                TargetJoinGame(true, _matchID, playerIndex, song, difficulty);
            }
            else
            {
                Debug.Log($"<color = red>Game Joined Unsuccessfully</color>");
                TargetJoinGame(false, _matchID, playerIndex, 1, 1);
            }
        }

        [TargetRpc]
        void TargetJoinGame(bool success, string _matchID, int _playerIndex, int songNumber, int gameDifficulty)
        {
            playerIndex = _playerIndex;
            matchID = _matchID;
            PlayerPrefs.SetInt("songNumber", songNumber);
            PlayerPrefs.SetInt("gameDifficulty", gameDifficulty);
            Debug.Log($"song number {PlayerPrefs.GetInt("songNumber")}");
            Debug.Log($"game difficulty {PlayerPrefs.GetInt("gameDifficulty")}");
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
            if (!isOwned) return;

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



        // update possible score to server
        [Client]
        public void UpdatePossibleScore(int score)
        {
            if (!isOwned) return;

            possibleScore = score;
            CmdUpdatePossibleScore(score);
        }

        [Command]
        void CmdUpdatePossibleScore(int score)
        {
            possibleScore = score;
        }

        // update possible score to clients
        void possibleScoreUpdated(int oldScore, int newScore)
        {
            Debug.Log($"NEW POSSIBLE SCORE {newScore}");
            score_display_v2.instance.UpdatePossiblePoint(newScore);
        }

        [Server]
        void possibleScoreUpdated(int score)
        {
            RpcPossibleScoreUpdated(score);
        }

        [ClientRpc]
        void RpcPossibleScoreUpdated(int score)
        {
            score_display_v2.instance.UpdatePossiblePoint(score);
        }

        // update raw score to server
        [Client]
        public void UpdateRawScore(int score)
        {
            if (!isOwned) return;

            playerRawScore = score;
            CmdUpdateRawScore(score);
        }

        [Command]
        void CmdUpdateRawScore(int score)
        {
            playerRawScore = score;
        }






        // update score to clients
        //[SyncVar(hook = nameof(ScoreUpdated))] int score;

        void RawScoreUpdated(int oldScore, int newScore)
        {
            Debug.Log($"NEW SCORE {newScore}");
            score_display_v2.instance.UpdateRawPoint(newScore);
        }

        [Server]
        void ScoreRawUpdated(int score)
        {
            RpcRawScoreUpdated(score);
        }

        [ClientRpc]
        void RpcRawScoreUpdated(int score)
        {
            score_display_v2.instance.UpdateRawPoint(score);
        }


        //power up

        [Client]
        void SendMessageTo(GameObject target, string message)
        {
            CmdSendMessageTo(target, message);
        }

        [Command]
        void CmdSendMessageTo(GameObject target, string message)
        {
            target.GetComponent<Player>().ReceiveMessage(message);
        }

        [Server]
        void ReceiveMessage(string message)
        {
            Debug.Log("RECEIVED X on server");
            TargetReceiveMessage(message);
        }

        [TargetRpc]
        void TargetReceiveMessage(string message)
        {
            // Add function to do
            Debug.Log("RECEIVED X");
            //score_display_v2.instance.randomDisplay();
            Debug.Log($"message is {message}");
            if(message == "left")
            {
                obstacleSpawner.instance.spawnLeft();
            }
            else
            {
                obstacleSpawner.instance.spawnRight();
            }
            
        }
    }

}
