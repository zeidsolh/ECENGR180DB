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

namespace MirrorBasics
{
    public class VoiceMovement : MonoBehaviour
    {
        private KeywordRecognizer keywordRecognizer;
        private Dictionary<string, Action> actions = new Dictionary<string, Action>();

        void Start()
        {
            actions.Add("continue", Continue);
            actions.Add("play", Play);

            keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
            keywordRecognizer.Start();
        }

        private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
        {
            Debug.Log(speech.text);
            actions[speech.text].Invoke();
        }

        private void Continue()
        {
            endScreen.instance.ContinueFunction();
        }

        private void Play()
        {
            Debug.Log("PLAY");
            MainMenu.instance.OnButtonPress();
        }
    }
}