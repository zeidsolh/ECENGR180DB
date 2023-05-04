/*
 Description: changeColor.cs is attached to the target prefab to change its color to green whenever it enters the Hitbox Trigger Box region
to indicate to players that they can hit the target.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (rend != null)
        {
            //rend.material.color = Color.green;
            if (other.CompareTag("Trigger Box"))
            {
                rend.material.color = Color.green;
            }

        }

    }

    void OnTriggerExit(Collider other)
    {
        //rend.material.color = Color.black;
        if (other.CompareTag("Trigger Box"))
        {
            rend.material.color = Color.black;
        }
    }
}