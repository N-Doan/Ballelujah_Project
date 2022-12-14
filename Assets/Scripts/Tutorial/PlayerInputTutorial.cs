using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTutorial : MonoBehaviour
{
    [SerializeField]
    private TutorialManager manager;

    public void onTowerMenuOpened()
    {
        manager.onTowerMenuOpened();
    }

    public void onTowerBuilt()
    {
        manager.onTowerBuilt();
    }

    public void onUpgradeMenuOpened()
    {
        manager.onUpgradeMenuOpened();
    }

    public void onTowerUpgraded()
    {
        manager.onTowerUpgraded();
    }

    public void onNomadAbilityUsed()
    {
        manager.onNomadAbilityUsed();
    }


}
