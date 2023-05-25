using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;


namespace MirrorBasics
{

    public class UILobby : MonoBehaviour
    {

        public static UILobby instance;

        [Header("Host Join")]

        [SerializeField] InputField joinMatchInput;
        [SerializeField] Button joinButton;
        [SerializeField] Button hostButton;
        [SerializeField] GameObject connectScreen;
        [SerializeField] Canvas lobbyCanvas;


        [Header("Lobby")]
        [SerializeField] Transform UIPlayerParent;
        [SerializeField] GameObject UIPlayerPrefab;
        [SerializeField] Text matchIDText;
        [SerializeField] GameObject beginGameButton;

        void Start()
        {
            instance = this;
        }

        public void Host()
        {
            joinMatchInput.interactable = false;
            joinButton.interactable = false;
            hostButton.interactable = false;

            Player.localPlayer.HostGame();

        }

        public void HostSuccess(bool success, string matchID)
        {
            if (success)
            {
                lobbyCanvas.enabled = true;
                connectScreen.SetActive(false);
                SpawnUIPlayerPrefab(Player.localPlayer);
                matchIDText.text = matchID;
                beginGameButton.SetActive(true);
            }
            else
            {
                joinMatchInput.interactable = true;
                joinButton.interactable = true;
                hostButton.interactable = true;
            }
        }

        public void Join()
        {
            joinMatchInput.interactable = false;
            joinButton.interactable = false;
            hostButton.interactable = false;

            Player.localPlayer.JoinGame(joinMatchInput.text.ToUpper());
        }

        public void JoinSuccess(bool success, string matchID)
        {
            if (success)
            {
                lobbyCanvas.enabled = true;
                connectScreen.SetActive(false);
                SpawnUIPlayerPrefab(Player.localPlayer);
                matchIDText.text = matchID;
            }
            else
            {
                joinMatchInput.interactable = true;
                joinButton.interactable = true;
                hostButton.interactable = true;
            }
        }


        public void SpawnUIPlayerPrefab(Player player)
        {
            GameObject newUIPlayer = Instantiate(UIPlayerPrefab, UIPlayerParent);
            newUIPlayer.GetComponent<UIPlayer>().SetPlayer(player);
            newUIPlayer.transform.SetSiblingIndex(player.playerIndex - 1);
        }

        public void BeginGame()
        {
            lobbyCanvas.enabled = false;
            Player.localPlayer.BeginGame();
        }

        public void DisableConnect()
        {
            lobbyCanvas.enabled = false;
            connectScreen.SetActive(false);
        }

    }

}