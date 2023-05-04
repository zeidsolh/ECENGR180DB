/* 
 Description: obstacleSpawner.cs is used to instantiate and transform 
(move) obstacles towards the player camera in one of two lanes 
(left or right) at speed "speed"
    o for right lane
    z for left lane
Script should be attached to empty obstacleSpawner object in Sample Scene
obstacle prefab must be dragged from assets into the object field in the inspector window
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class obstacleSpawner : MonoBehaviour
{
    GameObject inputObject; // to access curSong.speed from class Spawner in Spawner.cs attached to G.O. Spawner
    Spawner inputSpawner;
    public GameObject obstacle;
    public List<float> timeSinceSpawn;
    public List<GameObject> obstacles = new List<GameObject>();
    public Vector3 obs_startPositionLeft = new Vector3(-0.8f, 1.6f, 50.0f);
    public Vector3 obs_startPositionRight = new Vector3(0.8f, 1.6f, 50.0f);
    public Vector3 obs_finalPositionLeft = new Vector3(-0.8f, 1.6f, 1.9f);
    public Vector3 obs_finalPositionRight = new Vector3(0.8f, 1.6f, 1.9f);
    public float os;
    public float speed;
    public float elapsedTime;
    public static obstacleSpawner instance;


    private void Awake()
    {

        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        os = Time.time;   // offset from menu screen

        inputObject = GameObject.Find("Spawner");
        inputSpawner = inputObject.GetComponent<Spawner>();



        // speed = 0.1f;   // 0.1f - 0.4f
    }

    // Update is called once per frame
    void Update()
    {
        speed = inputSpawner.speed;
        //Debug.Log("Obstacle speed = " + speed);

        elapsedTime = Time.time - os;

        //// If player presses "o" send an obstacle on the right side
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    spawnLeft();
        //}

        //// If a player presses "z" send an obstacle on the left side
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    spawnRight();
        //}

        // If obstacles container is not empty, move all obstacles forward
        for (int f = 0; f < obstacles.Count; f++)
        {
            GameObject obstacle = obstacles[f];
            if (obstacle == null)
            {
                continue;
            }

            Vector3 obstacleCurrentPosition = obstacle.transform.position;

            // Determine startPoint and endPoint for current obstacle
            Vector3 obstacleStartPoint = obs_startPositionRight;    // default
            Vector3 obstacleEndPoint = obs_finalPositionRight;  // default

            float xCoord = obstacle.transform.position.x;
            if (xCoord == -0.8f)
            {
                //Debug.Log("Current obstacle position Left ----------------");
                obstacleStartPoint = obs_startPositionLeft;
                obstacleEndPoint = obs_finalPositionLeft;
            }
            else if (xCoord == 0.8f)
            {
                //Debug.Log("Current obstacle position Right ----------------");
                obstacleStartPoint = obs_startPositionRight;
                obstacleEndPoint = obs_finalPositionRight;
            }

            // Transform the current obstacle to obstacleCurrentPosition using the temporary start and end points above
            obstacleCurrentPosition = obstacleStartPoint + (obstacleEndPoint - obstacleStartPoint) * ((elapsedTime - timeSinceSpawn[f]) * speed);
            obstacle.transform.position = obstacleCurrentPosition;
        }

    }
    public void spawnRight()
    {
        Debug.Log("user hit o");
        GameObject ob = Instantiate(obstacle, obs_startPositionRight, Quaternion.identity);
        obstacles.Add(ob);
        timeSinceSpawn.Add(elapsedTime);
    }

    public void spawnLeft()
    {
        Debug.Log("user hit z");
        GameObject ob = Instantiate(obstacle, obs_startPositionLeft, Quaternion.identity);
        obstacles.Add(ob);
        timeSinceSpawn.Add(elapsedTime);
    }

}
