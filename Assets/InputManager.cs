using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Keys pressedKey;
    public enum Keys
    {
        None,
        MouseX,
        MouseY,
        Space,
        LShift,
        GetAxisRaw,
        Escape,
        Z,
        F,
        G,
        R,
        C,
        Q,
        E,
        LClick,
        Rclick,
        MouseScroll,
    }
    // Use this for initialization
    void Start()
    {
        pressedKey = Keys.None;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            pressedKey = Keys.Z;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            pressedKey = Keys.None;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            pressedKey = Keys.Q;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            pressedKey = Keys.None;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            pressedKey = Keys.E;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            pressedKey = Keys.None;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            pressedKey = Keys.C;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            pressedKey = Keys.None;
        }

        NetworkSend.SendKeyInput(pressedKey);
    }
}
