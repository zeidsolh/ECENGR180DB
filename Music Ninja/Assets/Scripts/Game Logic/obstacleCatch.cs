/*
Description: obsctacleCatch removes obstacles from the obstacles list when they reach the trigger box behind the player
Script should be attached to Hitbox Triggerbox (1) in the Sample Scene.
obstacles Count must be decremented still
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class obstacleCatch : MonoBehaviour
{
    GameObject obsSpawnerObj;
    obstacleSpawner script;

    void Start()
    {
        obsSpawnerObj = GameObject.Find("ObstacleSpawner");
        script = obsSpawnerObj.GetComponent<obstacleSpawner>();

        //obstacleSpawner script;
        //script = ob
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Obstacle exited collider catch");
        if (other.CompareTag("obstaclePrefab"))
        {
            Destroy(other.gameObject);

            //script.obstacles.RemoveAt(script.obstacles.Count - 1);
        }
    }
}