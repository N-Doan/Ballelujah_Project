using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    PlayerController player;
    int choice;
    public Image menu1;
    public Image menu2;
    public Image menu3;
    public Image menu4;
    public Image menu5;
    public Image menu6;
    public Image menu7;
    public Image menu8;
    public Image menu9;

    GameObject[] buildSpots;
    // Start is called before the first frame update
    void Start()
    {
        buildSpots = player.towerPosition;

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showMenu() {

       
    }

    public void chooseTower() 
    { 
        
    }

    public int chooseBumper() {
        choice = 1;
        return choice;
    }

    public int chooseWind() 
    {
        choice = 4;
        return choice;
    }

    public int choosePortal() 
    {
        choice = 3;
        return choice;
    }

    public int chooseCannon() {
        choice = 2;
        return choice;
    }
}
