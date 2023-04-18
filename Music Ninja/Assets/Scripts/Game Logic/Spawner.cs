/*
Description:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;    // for reading input from .txt files for song sequencing, gesture/speech recognition, and localization
using MenuLogicNamespace;   // to access Menu



// Declare this class elsewhere
// Song Class

/*
public class Menu
{
    public void PausePlay()
    {
        // Check for user speech commands:
        // on_message("MusicNinja"..."
        // string msg = message.payload(2:-1);
        string msg = "start";   // for now;
        msg.ToLower();
        string confirm = "";
        // Move below code to Menu or UI 
        if (msg == "quit")
        {
            // pause the current game
            // display("Are you sure?);
            // display("Yes/No");
            // get user speech
            confirm = "yes"; // for now
            confirm.ToLower();
            if (confirm == "yes")
            {
                // Go to the Menu screen
            }
            else if (confirm == "no")
            {
                // resume
            }
        }
        else if (msg == "pause")
        {
            // pause the current game
            Time.timeScale = 0;
            Debug.Log("Set timescale to 0.");
            // display("Resume?")
            // display("Yes/No?)
            // get user speech
            confirm = "yes"; // for now
        }
        if (confirm == "yes")
        {
            // resume the game
            Time.timeScale = 1;
            //Debug.Log("Set timescale to 1.");
        }
        else if (confirm == "no")
        {
            // quit the game
        }
        else if (msg == "start")
        {
        }
    }
    public void displaySongList()
    {
        // write to on-screen display
        Debug.Log("Displaying Song List."); // for now
    }
    public void setSongChoice()
    {
        // get user speech
        songChoice = "Crab Rave";   // for now
    }
    public void setDifficulty()
    {
        // get user difficulty from speech recognition
        difficulty = "easy";   // for now
    }
    public int updateHighScores(Song song, int score)
    {
        return 0;
        // Write to local game file that stores an array of 10 x number of songs in song list
        // each song records the top 10 scores in a .txt file whenever a game is finished
    }
    private Song[] songList;
    //private List<Song> songList;
    private string songChoice;
    private string difficulty;
}
*/

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject SongBackground;
    [SerializeField] GameObject CurrentScoreDisplay;
    [SerializeField] GameObject EndScoreDisplay;
    [SerializeField] GameObject SpawnerObject;

    public float offset;
    //float startDelay = 7.1f;   // for now
    public GameObject prefab;
    public GameObject obstacle;
    public Transform[] points;
    //public float beat = (60f / 125f) * 2f;
    public float beat;  // = (60f / curSong.bpm()) * 2f;
    private float timer;
    public float speed;
    public float timeSinceLastSpawn = 0;
    bool delayInProgress = true;
    public int numTargetsSpawned = 0;

    // Declare containers for targets and their corresponding intervals
    public List<GameObject> targets = new List<GameObject>();
    public List<GameObject> activeTargets = new List<GameObject>();
    public List<GameObject> obstacles = new List<GameObject>();
    public List<float> timeBetweenEachNote = new List<float>();
    public List<Vector3> startPoints = new List<Vector3>();
    public List<Vector3> endPoints = new List<Vector3>();
    private List<float> speedList;

    public Vector3 startPosition = new Vector3(0, 1.6f, 50.0f);
    public Vector3 startPositionLeft = new Vector3(-0.8f, 1.6f, 50.0f);
    public Vector3 startPositionRight = new Vector3(0.8f, 1.6f, 50.0f);
    public Vector3 finalPosition = new Vector3(0, 1.6f, 1.9f);
    public Vector3 finalPositionLeft = new Vector3(-0.8f, 1.6f, 1.9f);
    public Vector3 finalPositionRight = new Vector3(0.8f, 1.6f, 1.9f);


    //public static string songChoice = "Crab Rave";    // for now
    public static string songChoice = "Crab Rave";
    public string sequenceKey = ""; // for now
    public Song curSong = new Song(songChoice, 125, 0.58f);
    //public Menu curMenu = new Menu();
    //MenuLogic curMenu;
    public int difficultySelected;
    private int maxLimit = 999;

    private bool test = true;

    //public float duration = 2.0f; // length of Crab Rave in seconds: 163
    void Start()
    {
        offset = Time.time;

        if (test)
            Debug.Log("Start Spawner");

        Invoke("LoadNextScene", 30.0f);  // For switching to the EndScreen scene

        //Debug.Log("Spawner::Start_____________________________________________");
        // Get song choice using speech recognition

        //Debug.Log("Initializing Menu...");
        //curMenu = new MenuLogic();
        //curMenu.init();
        //Debug.Log("Menu Initialized.");
        // Pause execution from Menu screen

        /*
        while (curMenu.inMenuScreen)
        {
            Debug.Log("Waiting for game paramters.");
            //break;
        }
        */

        //curSong =     // for now
        //Song curSong = new Song(songChoice, 125);    // for now

        // load the sequence from file into the song's key

        // Get difficulty
        difficultySelected = 1; // for now
        curSong.setDifficulty(difficultySelected);
        curSong.loadScript(ref curSong);
        speedList = new List<float>(){
            curSong.beat() / 8.0f, 
            curSong.beat() / 2f,
            curSong.beat(),
            curSong.beat() * 2
        };

        speed = speedList[difficultySelected-1]; // set speed
        //beat = (60f / curSong.bpm) * 2f;
        beat = (60f / curSong.bpm);

        if (test)
            Debug.Log("curSong Key: " + curSong.getKey() + "++++++++++++++++++++++++++++++++++");
        
        decodeAndPopulate(curSong.name(), curSong.getKey(), curSong); // Decode the song script and populate the following containers: targets, timeBetweenEachNote, startPoints, endPoints
        StartCoroutine(DelayedPause(curSong.getDelay())); // Delay execution to synchronize 1st target with the start of the song and call the DelayedPause coroutine with a startDelay second delay
    }


    void Update()
    {
        //Debug.Log("Targets count: " + targets.Count);
        // Pause?
        //curMenu.PausePlay();    // Check for start/pause/resume/quit


        // wait for song delay
        if (!delayInProgress)
        {
            //Debug.Log("Waiting to send first target...");
            // max targets container to 99 items
            if (targets.Count > maxLimit)
            {
                if (test)
                    Debug.Log("targets.Count countainer limit reached.");

                targets.RemoveAt(targets.Count - 1);
            }

            timeSinceLastSpawn += Time.deltaTime;
            if (numTargetsSpawned < targets.Count && timeSinceLastSpawn >= timeBetweenEachNote[numTargetsSpawned])
            {
                //foreach (var obstacle in obstacles)
                //{

                //}
                GameObject t = targets[numTargetsSpawned];
                activeTargets.Add(t);
                timeSinceLastSpawn = 0;
                numTargetsSpawned++;
            }
            move(numTargetsSpawned); // spawn a new target and move all existing targets forward a bit

        }
    }

    // Utility Functions
    IEnumerator DelayedPause(float delay)
    {
        yield return new WaitForSeconds(delay);
        delayInProgress = false;
    }

    void decodeAndPopulate(string songName, string sequence, Song curSong)
    {
        //Debug.Log("Entered dap function. _-------------------------------------------------------");
        //Debug.Log("songName is " + songName);
        // Transcribe the sequence key and populate the "targets" and "timeBetweenEachNote" containers appropriately
        if (songName != "" && sequence != "")
        {
            if (test)
                Debug.Log("if statement 1.");


            //Debug.Log("SEQ: " + sequence);
            string sequenceKey = sequence;
            //Debug.Log("SequenceKey length" + sequenceKey.Length);
            //Debug.Log("SeqKey: " + sequenceKey + "+++++++++++++++++++++++++++++++++++++++++++++++");
            // Clear containers
            targets.Clear();
            timeBetweenEachNote.Clear();
            startPoints.Clear();
            endPoints.Clear();

            // declare temporary variables for position, rotation and interval
            Quaternion rotation = Quaternion.Euler(0, 0, 180);  // default
            Vector3 position = startPosition;   // default
            Vector3 positionFinal = finalPosition;  // default
            //float interval = quarterNote;   // default
            float interval = curSong.beat();   // default

            for (int i = 0; i < sequenceKey.Length - 3; i++)
            {
                // set position variable
                if (sequenceKey[i + 3] == ',')
                {
                    if (sequenceKey[i] == 'l')
                    {
                        position = startPositionLeft;
                        positionFinal = finalPositionLeft;
                    }
                    else if (sequenceKey[i] == 'r')
                    {
                        position = startPositionRight;
                        positionFinal = finalPositionRight;
                    }
                    else if (sequenceKey[i] == 'm')
                    {
                        position = startPosition;   // middle
                        positionFinal = finalPosition;
                    }
                }
                // set rotation variable
                else if (sequenceKey[i + 2] == ',')
                {
                    // set rotation variable
                    if (sequenceKey[i] == 'l')
                    {
                        rotation = Quaternion.Euler(0, 0, 270);
                    }
                    else if (sequenceKey[i] == 'r')
                    {
                        rotation = Quaternion.Euler(0, 0, 90);
                    }
                    else if (sequenceKey[i] == 'u')
                    {
                        rotation = Quaternion.Euler(0, 0, 180);
                    }
                    else if (sequenceKey[i] == 'd')
                    {
                        rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else if (sequenceKey[i] == 'o')
                    {
                        //GameObject ob = Instantiate(obstacle, position, Quaternion.identity);
                        //obstacles.Add(ob);
                        if (test)
                            Debug.Log("Add obstacle here");
                    }
                }
                // set time since previous note
                else if (sequenceKey[i + 1] == ',')
                {
                    int c = sequenceKey[i] - '0';
                    // set the temporary interval variable appropriately for the next target instantiation
                    if ((0 < c) && (c <= 4))
                    {
                        // (60 / bpm) / ( 2 ^ i )
                        interval = curSong.beat() / Mathf.Pow(2, c);
                        interval *= 2f;
                    }
                    else if ((4 < c) && (c <= 9))
                    {
                        interval = curSong.beat() * (c - 5);
                        interval *= 2f;
                    }
                    else if (c == 0)
                    {
                        interval = curSong.beat() / 3.0f;  // triplet
                        interval *= 2f;
                    }

                }
                // Populate containers w/ objects + data
                else if (sequenceKey[i] == ',')
                {
                    //Debug.Log("interval cur value: " + interval);
                    // Instantiate a prefab target game object with the temporary fields
                    GameObject target = Instantiate(prefab, position, rotation);

                    // Add the game object to the "targets" container
                    targets.Add(target);


                    // Update the "startPoints" and "endPoints" containers
                    startPoints.Add(position);
                    endPoints.Add(positionFinal);

                    // Update the "timeBetweenEachNote" container by adding the current interval to the end of the list
                    timeBetweenEachNote.Add(interval);
                    continue;
                }

            }
        }
    }

    void move(int numTargets)
    {
        //float elapsedTime = Time.time - startDelay;
        float elapsedTime = Time.time - curSong.getDelay() - offset;
        //Debug.Log(numTargets);
        for (int i = 0; i < numTargets; i++) // add time condition
        {

            if (targets[i] != null) // memory leak atm? (targets destroyed but container size not decremented)
            {
                GameObject target = activeTargets[i];
                Vector3 currentPosition = target.transform.position;
                //Debug.Log("Moving target" + i); 
                // obstacles
                /*
                int f;
                int m = obstacles.Count;
                if (i < m)
                {
                    f = m-1;
                } else
                {
                    f = i;
                }
                GameObject obstacle = obstacles[f];
                Vector3 obstacleCurrentPosition = obstacle.transform.position;
                Vector3 obstacleStartPoint = startPoints[i];
                Vector3 obstacleEndPoint = endPoints[i];
                */

                Vector3 startPoint = startPoints[i];
                Vector3 endPoint = endPoints[i];



                currentPosition = startPoint + (endPoint - startPoint) * ((elapsedTime - timeSinceFirstSpawn(i)) * speed);
                target.transform.position = currentPosition;

                //obstacleCurrentPosition = obstacleStartPoint + (obstacleEndPoint - obstacleStartPoint) * ((elapsedTime - timeSinceFirstSpawn(i)) * speed);
                //obstacle.transform.position = obstacleCurrentPosition;

            }
        }
    }
    float timeSinceFirstSpawn(int n)
    {
        float timeSinceFirstSpawn = 0.0f;
        for (int j = 0; j < n; j++)
        {

            timeSinceFirstSpawn += timeBetweenEachNote[j];
        }
        return timeSinceFirstSpawn;

    }

    void LoadNextScene()
    {
        //score = getInput.playerscore;
        //SceneManager.LoadScene("EndScreen");
        SceneManager.LoadScene("EndScreen", LoadSceneMode.Additive);
        SongBackground.SetActive(false);
        CurrentScoreDisplay.SetActive(false);
        EndScoreDisplay.SetActive(true);
        SpawnerObject.SetActive(false);

    }


}