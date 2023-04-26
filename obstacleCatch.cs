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
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("obstaclePrefab"))
        {
            Destroy(other.gameObject);
            //obstacles.RemoveAt(obstacles.size() - 1);
        }
    }
}
