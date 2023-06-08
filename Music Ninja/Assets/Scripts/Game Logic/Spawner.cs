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
    public GameObject prefab2;
    public GameObject obstacle;
    public Transform[] points;
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
    private List<(Vector3, Vector3)> positionList;
    private List<Quaternion> rotationList;

    public Vector3 startPosition = new Vector3(0, 1.6f, 50.0f);
    public Vector3 startPositionLeft = new Vector3(-0.8f, 1.6f, 50.0f);
    public Vector3 startPositionRight = new Vector3(0.8f, 1.6f, 50.0f);
    public Vector3 finalPosition = new Vector3(0, 1.6f, 1.9f);
    public Vector3 finalPositionLeft = new Vector3(-0.8f, 1.6f, 1.9f);
    public Vector3 finalPositionRight = new Vector3(0.8f, 1.6f, 1.9f);


    //public static string songChoice = "Crab Rave";    // for now
    public static string songChoice = "test";
    public Sequence seq = new Sequence();
    public Song curSong;
    public int difficultySelected;
    private int maxLimit = 999;

    private bool test = true;

    //public float duration = 2.0f; // length of Crab Rave in seconds: 163
    void Start()
    {
        offset = Time.time;

        if (test)
            Debug.Log("Start Spawner");
        float duration = 30f; // duration of the scene is 30 sec by default
        int song_num;
        song_num = PlayerPrefs.GetInt("songNumber", 1);
        if (song_num == 1)
        {
            duration = 88.0f;   // 163.0f;
            songChoice = "test";
        }
        else if (song_num == 2)
        {
            duration = 242.0f;
            songChoice = "Disconnected";
        }
        else if (song_num == 3)
        {
            duration = 219.0f;
            songChoice = "Flight";
        }
        else if (song_num == 4)
        {
            duration = 358.0f;
            songChoice = "Every Language is Alive";
        }
        else if (song_num == 5)
        {
            duration = 249.0f;
            songChoice = "Unity";
        }
        else if (song_num == 6)
        {
            duration = 281.0f;
            songChoice = "Breathing Underwater";
        }

        // Get difficulty
        difficultySelected = PlayerPrefs.GetInt("gameDifficulty", 1);
        if (songChoice == "Unity" && difficultySelected == 1)
        {
            Debug.Log("Shortening Runwar...");
            startPosition = new Vector3(0, 1.6f, 20.0f);
            startPositionLeft = new Vector3(-0.8f, 1.6f, 20.0f);
            startPositionRight = new Vector3(0.8f, 1.6f, 20.0f);
        }



        Invoke("LoadNextScene", duration);  // Switch to EndScreen scene after the duration specified

        // Get song choice using speech recognition

        // Get difficulty
        difficultySelected = PlayerPrefs.GetInt("gameDifficulty", 1);
        positionList = new List<(Vector3 start, Vector3 end)>()
        {
            (startPositionLeft, finalPositionLeft),
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
        curSong = seq.getSong(songChoice);
        speed = curSong.speedList[difficultySelected - 1]; // set speed

        // Decode the song script and populate the following containers: targets, timeBetweenEachNote, startPoints, endPoints
        decodeAndPopulate(curSong);

        // Delay execution to synchronize 1st target with the start of the song and call the DelayedPause coroutine with a startDelay second delay
        StartCoroutine(DelayedPause(curSong.startDelay[difficultySelected - 1]));
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

    void decodeAndPopulate(Song songData)
    {
        if (songData.name != "" && songData.lane[0] != 0)
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
            float interval = songData.beat;   // default

            // Transcribe the sequence key and populate the "targets" and "timeBetweenEachNote" containers appropriately
            for (int i = 0; i < songData.lane.Count - 1; i++)
            {
                (position, positionFinal) = positionList[songData.lane[i] - 1]; // set positions for game object
                rotation = rotationList[songData.direction[i] - 1]; // set direction of gesture for game object

                // set the correct spawn rate for game object
                switch (songData.rate[i])
                {
                    case 0:
                        interval = songData.beat / 3.0f * 2;
                        break;
                    case int n when (n > 0 && n <= 4):  // ???
                        interval = songData.beat / Mathf.Pow(2, (n + 1)); // ???
                        break;
                    case int n when (n <= 9): // ???
                        interval = songData.beat * (n - 5);
                        
                        // Add random chance of 1/8 note instead of quarter note w/ less likelyhood on hard then medium
                        if (difficultySelected == 2 && n == 7)
                        {
                            int r = Random.Range(0, 2);
                            if(r == 1)
                            {
                                interval = songData.beat * (n - 5) / 2;
                            }
                        }
                        else if (difficultySelected == 3 && n == 7)
                        {
                            int r = Random.Range(0, 7);
                            if (r == 1)
                            {
                                interval = songData.beat * (n - 5) / 2;
                            }
                        }
                        
                        break;
                    default:
                        Debug.Log($"Should never reach here");
                        break;
                }

                if (interval == 0) {
                    Debug.Log($"{songData.rate[i]} gives 0");
                    Application.Quit();
                }

                if (test)
                        Debug.Log($"Index {i}, on lane {songData.lane[i]}, position {position}, positionFinal {positionFinal}, rotation {rotation}, rate {songData.rate[i]}, interval {interval}");

                int randomInt = Random.Range(1, 3);
                if (PlayerPrefs.GetInt("gameDifficulty", 1) == 1)
                {
                    randomInt = 1;
                }
                if (randomInt == 1)
                {
                    GameObject t = Instantiate(prefab, position, rotation);    // orange target (right hand)
                    targets.Add(t); // add game object to "targets" container
                    startPoints.Add(position); // update start point of game object
                    endPoints.Add(positionFinal); // update end point of game object
                    timeBetweenEachNote.Add(interval); // update rate of game object spawn
                }
                else if (randomInt == 2)
                {
                    GameObject t = Instantiate(prefab2, position, rotation);   // blue target (left hand)
                    targets.Add(t); // add game object to "targets" container
                    startPoints.Add(position); // update start point of game object
                    endPoints.Add(positionFinal); // update end point of game object
                    timeBetweenEachNote.Add(interval); // update rate of game object spawn
                }
            }
        }
    }

    void move(int numTargets)
    {
        //float elapsedTime = Time.time - startDelay;
        float elapsedTime = Time.time - curSong.startDelay[difficultySelected - 1] - offset;
        //Debug.Log(numTargets);
        for (int i = 0; i < numTargets; i++) // add time condition
        {
            if (targets[i] != null) // memory leak atm? (targets destroyed but container size not decremented)
            {
                GameObject target = activeTargets[i];
                Vector3 currentPosition = target.transform.position;

                Vector3 startPoint = startPoints[i];
                Vector3 endPoint = endPoints[i];

                currentPosition = startPoint + (endPoint - startPoint) * ((elapsedTime - timeSinceFirstSpawn(i)) * speed);
                target.transform.position = currentPosition;
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