using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;    // for reading input from .txt files for song sequencing, gesture/speech recognition, and localization
using MenuLogicNamespace;
using UnityEngine.UI;
using System;

public class NoseLocalization : MonoBehaviour
{
    private string outputPath = "../Gesture/gesturefile.txt";
    public int noseX { get; set; }
    public int noseY { get; set; }
    private Camera cam;
    private float camerawidth = 640f;
    private float cameraheight = 480f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting Move Object");
        noseX = 0;
        noseY = 0;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        updateNosePosition();
        //Debug.Log("Moving Object");
        //this is the other method
        // float objectX=((float)noseX/640f)-0.5f;
        // float objectY=((float)noseY/480f)-0.5f;
        // objectX=objectX*(-1.84f/0.5f);
        // objectY=objectY*(-2.411f/0.5f);
        // Debug.Log(noseX);

        //this is for the camera method

        float screenX = (float)noseX * ((float)Screen.width / camerawidth);
        float screenY = (float)noseY * ((float)Screen.height / cameraheight);
        //Debug.Log(Screen.height);
        //Debug.Log(screenY);
        Vector3 screenPosition = new Vector3((float)screenX, (float)screenY, 1f);
        //Debug.Log(screenPosition);
        transform.position = cam.ScreenToWorldPoint(screenPosition);
        //Debug.Log(transform.position);

        //this is the other method
        // Vector3 newPosition=new Vector3(objectX,objectY,2.5081f);
        // Debug.Log(newPosition);
        // transform.position=newPosition;

    }

    void updateNosePosition()
    {
        var lines = System.IO.File.ReadAllLines(@outputPath);
        var size = lines.Length;
        if(size == 0)
        {
            return;
        }
        var lastline = lines[size-1];
        string[] data = lastline.Split(',');
        noseX = Int32.Parse(data[2]);
        noseY = Int32.Parse(data[3]);
        if (noseX < 0)
            noseX = 0;
        if (noseY < 0)
            noseY = 0;
        //this is for the camera method
        noseX = (int)camerawidth - noseX;
        noseY = (int)cameraheight - noseY;
    }
}