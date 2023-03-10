
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class GameManager : MonoBehaviour
{
    public Dictionary<int, GameObject> playerList = new Dictionary<int, GameObject>();

    public static GameManager instance;
     
    public static TextMeshProUGUI textbox;

    public static Queue<string> messages = new Queue<string>();

    private void Awake()
    {
        instance = this;
        textbox = GameObject.Find("Messages").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (messages.Count > 0)
        {
                textbox.text += messages.Dequeue();

        }

    }

    internal float WrapEulerAngles(float rotation)
    {
        rotation %= 360;
        if (rotation >= 180)
            return -360;
        return rotation;
    }
    public float UnwrapEulerAngles(float rotation)
    {
        if (rotation >= 0)
            return rotation;

        rotation = -rotation % 360;
        return 360 - rotation;
    }
}
