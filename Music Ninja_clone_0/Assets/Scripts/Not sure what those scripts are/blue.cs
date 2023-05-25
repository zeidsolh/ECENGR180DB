using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blue : MonoBehaviour
{
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        rend.material.color = Color.blue;
    }

    void OnTriggerExit(Collider other)
    {
        rend.material.color = Color.black;
    }
}
