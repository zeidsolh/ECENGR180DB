using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleCollisionDetection : MonoBehaviour
{
    GameObject inputObject;
    getInput inputScript;

    void Start()
    {
        inputObject = GameObject.Find("Hitbox Trigger Box");
        inputScript = inputObject.GetComponent<getInput>();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("obstaclePrefab"))
        {
            //Debug.Log("GET OUT OF THE OBSTACLE! ++++++++++++++++++++++++++++++++++++");
            inputScript.points_lost_to_obstacles++;
        }
    }
}