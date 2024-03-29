using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public GameObject prefab;
    public int myConnectionID;

    public static NetworkManager instance;

    private Queue<int> _IDToSet = new Queue<int>();

    public Queue<MovementOrder> _ObjectToMove = new Queue<MovementOrder>();

    private bool _isMyPlayer = false;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //Must Manipulate Objects within Main thread!
        while(_IDToSet.Count > 0)
        {
            int id = _IDToSet.Dequeue();

            GameObject player = Instantiate(prefab, transform);
            player.name = "Player: " + _IDToSet;
            GameManager.instance.playerList.Add(id, player);

            if (_isMyPlayer)
                player.AddComponent<InputManager>();
        }

        while(_ObjectToMove.Count > 0)
        {
            MovementOrder order = _ObjectToMove.Dequeue();

            Debug.Log(order.obj.transform.position);

            order.obj.transform.position = order.movement;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        NetworkConfig.InitNetwork();      
    }


    private void OnApplicationQuit()
    {
        NetworkConfig.DisconnectFromServer();
    }

    public void InstantiateNetworkPlayer(int connectionID, bool isMyPlayer)
    {   
        _IDToSet.Enqueue(connectionID);
        _isMyPlayer = isMyPlayer;
    }
}
