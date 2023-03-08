using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public GameObject prefab;
    public int myConnectionID;

    public static NetworkManager instance;

    //This needs to be a queue later
    private bool _ThingsToSpawn = false;
    private int _IDToSet = 0;
    private bool _isMyPlayer = false;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //Must instantiate Objects within Main thread!
        if (_ThingsToSpawn)
        {
            GameObject thing = Instantiate(prefab);
            thing.name = "Player: " + _IDToSet;
            GameManager.instance.playerList.Add(_IDToSet, thing);

            if (_isMyPlayer)
                thing.AddComponent<InputManager>();

            _ThingsToSpawn = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        NetworkConfig.InitNetwork();
        NetworkConfig.ConnectToServer();
    }

    private void OnApplicationQuit()
    {
        NetworkConfig.DisconnectFromServer();
    }

    public void InstantiateNetworkPlayer(int connectionID, bool isMyPlayer)
    {   
        _ThingsToSpawn = true;
        _IDToSet = connectionID;
        _isMyPlayer = isMyPlayer;
       

    }
}
