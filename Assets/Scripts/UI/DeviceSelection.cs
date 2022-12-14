using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceSelection : MonoBehaviour
{
    [SerializeField]
    private GameObject p1GamePad;
    [SerializeField]
    private GameObject p1Keyboard;
    [SerializeField]
    private GameObject p2GamePad;
    [SerializeField]
    private GameObject p2Keyboard;
   

    public void cleanP1()
    {
        p1GamePad.SetActive(false);
        p1Keyboard.SetActive(false);
    }
    public void cleanP2()
    {
        p2GamePad.SetActive(false);
        p2Keyboard.SetActive(false);
    }
    public void cleanBoth()
    {
        cleanP1();
        cleanP2();
    }
    public void p1Gamepad()
    {
        p1GamePad.SetActive(true);
    }

    public void p1KeyBoard()
    {
        p1Keyboard.SetActive(true);
    }

    public void p2Gamepad()
    {
        p2GamePad.SetActive(true);
    }

    public void p2KeyBoard()
    {
        p2Keyboard.SetActive(true);
    }
}
