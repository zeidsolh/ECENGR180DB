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
            actions.Add("continued", Continue);
            actions.Add("tin you", Continue);

            actions.Add("play", Play);
            actions.Add("lay", Play);

            actions.Add("attack", PowerUp);
            actions.Add("tac", PowerUp);
            actions.Add("attached", PowerUp);
            actions.Add("attacked", PowerUp);
            actions.Add("the tech", PowerUp);
            actions.Add("the stack", PowerUp);
            actions.Add("patek", PowerUp);
            actions.Add("the tack", PowerUp);
            actions.Add("kotak", PowerUp);
            actions.Add("batac", PowerUp);
            actions.Add("as heck", PowerUp);
            
            actions.Add("stop", Stop);
            actions.Add("quit", Stop);
            actions.Add("new song", Stop);
            actions.Add("next", Stop);
            actions.Add("quick", Stop);
            actions.Add("new", Stop);
            actions.Add("exit", Stop);
            actions.Add("escape", Stop);

            actions.Add("tutorial", Tutorial);
            actions.Add("torial", Tutorial);

            actions.Add("options", Options);
            actions.Add("option", Options);
            actions.Add("portion", Options);

            actions.Add("Back", Back);
            actions.Add("ack", Back);
            actions.Add("hack", Back);
            actions.Add("tack", Back);

            keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(),ConfidenceLevel.Low);
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

        private void PowerUp()
        {
            Debug.Log("attack");
            Player.localPlayer.sendObstacle("left");
            //Player.localPlayer.sendObstacle("right");
        }

        private void Stop()
        {
            Debug.Log("Stop function tabled");
            //SceneManager.LoadScene("EndScreen");
        }

        private void Options()
        {
            Debug.Log("Options");
            OptionsScript.instance.OptionsButton();
        }

        private void Tutorial()
        {
            Debug.Log("Tutorial");
            tutorialButtons.instance.TutorialButton();
        }

        private void Back()
        {
            Debug.Log("Back");
            OptionsScript.instance.BackOptions();
            tutorialButtons.instance.BackTutorial();
        }
    }
}