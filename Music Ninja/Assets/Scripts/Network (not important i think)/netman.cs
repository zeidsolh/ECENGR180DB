using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class netman : NetworkManager
{
    public override void OnStartServer()
    {
        Debug.Log("server started");
    }
    public override void OnStopServer()
    {
        Debug.Log("server stopped");
    }
    public override void OnClientConnect()
    {
        Debug.Log("connected to server");
    }
    public override void OnClientDisconnect()
    {

    }

}
