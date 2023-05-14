/*
Description:
    VoiceMovement.cs is attached to the GameObject "SpeechRecog" and is responsible for recognizing spoken player commands
    such as "continue" and "play" for navigating between scenes.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;
using MirrorBasics; // to tell Player that an obstacle was sent

public class VoiceMovement : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private Player player;  // to set send_obstacle flag

    void Start()
    {
        actions.Add("continue", Continue);
        actions.Add("play", Play);
        actions.Add("go", Go);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

        player = new Player();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Continue()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Play()
    {
        Debug.Log("PLAY");
        SceneManager.LoadScene("SampleScene");  // (Crab Rave)
    }

    private void Go()
    {
        player.send_obstacle = true;
    }
}