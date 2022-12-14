using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTowerUpgradePath : MonoBehaviour
{
    //Starts at 0, should end at 3
    [SerializeField]
    public int currentUpgrade;
    [SerializeField]
    public GameObject finalTowerVFX;


    //returns true when the tower's current upgrade = 3
    public bool isFullyUpgraded()
    {
        if(currentUpgrade == 3)
        {
            return true;
        }
        return false;
    }

    //use this method to call other methods that apply the desired upgrade effect
    public void upgrade()
    {
        switch (currentUpgrade)
        {
            case 0:
                currentUpgrade++;
                applyUpgrade1();
                break;
            case 1:
                currentUpgrade++;
                applyUpgrade2();
                break;
            case 2:
                currentUpgrade++;
                applyUpgrade3();
                break;
        }
    }

    //override these with the tower's upgrade effects:
    public abstract void applyUpgrade1();

    public abstract void applyUpgrade2();

    public abstract void applyUpgrade3();

}
