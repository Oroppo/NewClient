using System;
using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Keys pressedKey;
    public float rotation;
    public enum Keys
    {
        None,
        W,
        A,
        S,
        D,
        Mouse1,
        Mouse2,
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
        CheckRotation();
        CheckCamera();
        CheckInput();

        rotation = GameManager.instance.UnwrapEulerAngles(transform.localEulerAngles.y);
    }
    //this is for a top-down solution will need to modify the raycast to work with a first person game.
    private void CheckCamera()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            transform.LookAt(pointToLook); 
        }
    }

    private void CheckRotation()
    {
        gameObject.transform.localEulerAngles =
        new Vector3(gameObject.transform.localEulerAngles.x,
        GameManager.instance.WrapEulerAngles(rotation),
        gameObject.transform.eulerAngles.z);

        NetworkSend.SendPlayerRotation(rotation);
    }

    private void CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            pressedKey = Keys.W;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            pressedKey = Keys.A;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            pressedKey = Keys.S;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            pressedKey = Keys.None;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            pressedKey = Keys.D;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            pressedKey = Keys.None;
        }


        NetworkSend.SendKeyInput(pressedKey);
    }
}
