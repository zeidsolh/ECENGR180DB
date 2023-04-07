using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.Netcode;
using UnityEngine.UI;
using Unity.Collections;





public class net : MonoBehaviour
{

    bool first = false;
    bool second = false;

    public List<ulong> serverConnections;


    public void RegisterMessages()
    {
        NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("UpdatePosition", UpdatePosition);
        NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("ReceiveAttack", ReceiveAttack);

        // Registering people who connect to server if (NetworkManager Singleton.IsServer)
        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("NEW ADDITION");
            NetworkManager.Singleton.OnClientConnectedCallback += ServerRegisterConnection;
        }
    }


    void OnDestroy()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= ServerRegisterConnection;
        NetworkManager.Singleton.CustomMessagingManager.UnregisterNamedMessageHandler("UpdatePosition");
        NetworkManager.Singleton.CustomMessagingManager.UnregisterNamedMessageHandler("ReceiveAttack");
    }

    public void SendPosition()
    {
        Debug.Log("SENTTTTTT");
        Vector3 sendPos = new Vector3(0, 123, 102);

        var man = NetworkManager.Singleton.CustomMessagingManager;
        var writer = new FastBufferWriter(FastBufferWriter.GetWriteSize(sendPos), Allocator.Temp);

        using (writer)
        {
            writer.WriteValueSafe(sendPos);
            man.SendNamedMessage("UpdatePosition", NetworkManager.ServerClientId, writer, NetworkDelivery.Reliable);
            Debug.Log("SENTTTTTT");
        }

    }

    public void UpdatePosition(ulong senderId, FastBufferReader messagePayLoad)
    {
        Vector3 recData;
        messagePayLoad.ReadValueSafe(out recData);
        Debug.Log(recData.ToString());
    }

    //receive

    [System.Serializable]
    public class AttackDamage
    {
        public int damage;
        public string target;
    }

    public void SendAttack()
    {
        AttackDamage data = new AttackDamage() { damage = 10, target = "Timmy" };
        byte[] serializedData = ToBytes(data);

        var man = NetworkManager.Singleton.CustomMessagingManager;
        var writer = new FastBufferWriter(FastBufferWriter.GetWriteSize(serializedData), Allocator.Temp);

        using (writer)
        {
            writer.WriteValueSafe(serializedData);
            man.SendNamedMessage("ReceiveAttack", serverConnections, writer, NetworkDelivery.Reliable);
        }
    }

    public void ReceiveAttack(ulong senderId, FastBufferReader messagePayload)
    {
        byte[] recData;
        messagePayload.ReadValueSafe(out recData);
        AttackDamage damage = (AttackDamage)ToObject(recData);
        Debug.Log(damage.target + " does " + damage.damage + " damage!");
    }


    //helpers
    public Text serverButton;
    public Text clientButton;
    public Text display;

    public void ServerRegisterConnection(ulong conn)
    {
        serverConnections.Add(conn);
    }

    public void StartNetworkManager(string startAs)
    {
        if (startAs == "server")
        {
            NetworkManager.Singleton.StartServer();
            serverButton.text = "Server Running...";
            clientButton.text = "";
        }
        else if (startAs == "client")
        {
            NetworkManager.Singleton.StartClient();
            serverButton.text = "";
            clientButton.text = "I am client...";
        }
        RegisterMessages();

    }

    public byte[] ToBytes(System.Object obj)
    {
        if (obj == null)
            return null;
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, obj);

        return ms.ToArray();
    }

    public System.Object ToObject(byte[] arrBytes)
    {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        System.Object obj = (System.Object)binForm.Deserialize(memStream);
        return obj;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!first && NetworkManager.Singleton.IsServer)
        {
            Debug.Log("IN SERVER");
            Debug.Log(first);
            first = true;
            StartNetworkManager("server");
        }
        if (!second && NetworkManager.Singleton.IsClient)
        {
            Debug.Log("I AM CLIENT");
            Debug.Log(second);
            second = true;
            StartNetworkManager("client");
        }

    }
}



