/*
Description: 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLightGrowth : MonoBehaviour
{
    private Light areaLight;
    private bool growing = true;

    private void Start()
    {
        areaLight = GetComponent<Light>();
    }

    private void Update()
    {
        if (growing)
        {
            areaLight.range += 2.5f;
            if (areaLight.range >= 250f)
            {
                growing = false;
            }
        }
        else
        {
            areaLight.range = 0.0f;
            if (areaLight.range <= 0f)
            {
                growing = true;
            }
        }
    }
}
