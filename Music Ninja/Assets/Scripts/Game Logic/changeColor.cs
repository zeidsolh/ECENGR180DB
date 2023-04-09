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
            rend.material.color = Color.green;
        }

    }

    void OnTriggerExit(Collider other)
    {
        rend.material.color = Color.black;
    }
}

