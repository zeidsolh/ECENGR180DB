/*
Description:
    Spawner.cs is responsible for instantiating targets and transforming them towards the player.
    Sequence data is passed in as sequenceKey string to a Song object which also contains bpm, song title, etc.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;    // for reading input from .txt files for song sequencing, gesture/speech recognition, and localization
using MenuLogicNamespace;   // to access Menu

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
    private List<(Vector3, Vector3)> positionList;
    private List<Quaternion> rotationList;

    public Vector3 startPosition = new Vector3(0, 1.6f, 50.0f);
    public Vector3 startPositionLeft = new Vector3(-0.8f, 1.6f, 50.0f);
    public Vector3 startPositionRight = new Vector3(0.8f, 1.6f, 50.0f);
    public Vector3 finalPosition = new Vector3(0, 1.6f, 1.9f);
    public Vector3 finalPositionLeft = new Vector3(-0.8f, 1.6f, 1.9f);
    public Vector3 finalPositionRight = new Vector3(0.8f, 1.6f, 1.9f);


    //public static string songChoice = "Crab Rave";    // for now
    public static string songChoice = "Crab Rave";
    public Song curSong = new Song(songChoice, 125, 0.58f);
    public int difficultySelected;
    private int maxLimit = 999;

    private bool test = true;

    //public float duration = 2.0f; // length of Crab Rave in seconds: 163
    void Start()
    {
        offset = Time.time;

        if (test)
            Debug.Log("Start Spawner");

        Invoke("LoadNextScene", 40.0f);  // Switch to EndScreen scene after the duration specified

        // Get song choice using speech recognition

        // Get difficulty
        difficultySelected = 1; // for now
        curSong.setDifficulty(difficultySelected);
        curSong.loadScript(ref curSong);
        speedList = new List<float>()
        {
            curSong.beat() / 8.0f, 
            curSong.beat() / 2f,
            curSong.beat(),
            curSong.beat() * 2
        };
        positionList = new List<(Vector3 start, Vector3 end)>() 
        {
            (startPositionLeft, finalPositionRight),
            (startPosition, finalPosition),
            (startPositionRight, finalPositionRight)
        };
        rotationList = new List<Quaternion>()
        {
            Quaternion.Euler(0, 0, 180),
            Quaternion.Euler(0, 0, 0),
            Quaternion.Euler(0, 0, 270),
            Quaternion.Euler(0, 0, 90)
        };

        // beat = (60f / curSong.bpm) * 2f;
        speed = speedList[difficultySelected-1]; // set speed
        beat = (60f / curSong.bpm);

        // Decode the song script and populate the following containers: targets, timeBetweenEachNote, startPoints, endPoints
        decodeAndPopulate(curSong.name(), curSong.getKey(), curSong);

        // Delay execution to synchronize 1st target with the start of the song and call the DelayedPause coroutine with a startDelay second delay
        StartCoroutine(DelayedPause(curSong.getDelay())); 
    }

    void Update()
    {
        // Check for pause/resume/quit

        // Wait for startDelay seconds to pass before spawing targets so that targets are synced to the song and the first targets is on beat
        if (!delayInProgress)
        {
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

    void decodeAndPopulate(string songName, Dictionary<string, List<float>> sequence, Song curSong)
    {
        if (songName != "" && sequence["lane"][0] != 0)
        {
            if (test)
                Debug.Log("if statement 1.");

            // Clear containers
            targets.Clear();
            timeBetweenEachNote.Clear();
            startPoints.Clear();
            endPoints.Clear();

            // Variables for position, rotation and interval of next target to be instantiated:
            Quaternion rotation = Quaternion.Euler(0, 0, 180);  // default
            Vector3 position = startPosition;   // default
            Vector3 positionFinal = finalPosition;  // default
            float interval = curSong.beat();   // default

            // Transcribe the sequence key and populate the "targets" and "timeBetweenEachNote" containers appropriately
            for (int i = 0; i < sequence["lane"].Count - 1; i++)
            {
                (position, positionFinal) = positionList[(int)sequence["lane"][i] - 1]; // set positions for game object
                rotation = rotationList[(int)sequence["direction"][i] - 1]; // set direction of gesture for game object

                // set the correct spawn rate for game object
                switch ((int)sequence["rate"][i])
                {
                    case 0:
                        interval = curSong.beat() / 3.0f * 2;
                        break;
                    case int n when (n > 0 && n <= 4):
                        interval = curSong.beat() / Mathf.Pow(2, n + 1);
                        break;
                    case int n when (n <= 9):
                        interval = curSong.beat() * (n - 5);
                        break;
                    default:
                        Debug.Log($"Should never reach here");
                        break;
                }
                
                if (test)
                    Debug.Log($"Index {i}, position {position}, positionFinal {positionFinal}, rotation {rotation}, interval {interval}");
                
                GameObject t = Instantiate(prefab, position, rotation);
                targets.Add(t); // add game object to "targets" container
                startPoints.Add(position); // update start point of game object
                endPoints.Add(positionFinal); // update end point of game object
                timeBetweenEachNote.Add(interval); // update rate of game object spawn
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
        SceneManager.LoadScene("EndScreen", LoadSceneMode.Additive);
        SongBackground.SetActive(false);
        CurrentScoreDisplay.SetActive(false);
        EndScoreDisplay.SetActive(true);
        SpawnerObject.SetActive(false);

    }
}