using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WinningScreen : MonoBehaviour
{
    public GameObject firstButton;
    EventSystem eventSystem;
    // Start is called before the first frame update
    void Awake()
    {
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(firstButton);
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }
}
