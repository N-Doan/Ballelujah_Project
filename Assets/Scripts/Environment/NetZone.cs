using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetZone : MonoBehaviour
{
    [SerializeField]
    private Transform[] respawnLocs;
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private bool side;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pinball")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/BallSink");
            Pinball p = other.gameObject.GetComponent<Pinball>();
            //P1's net zone
            if (side)
            {
                if (VariableSending.p2GamePad)
                {
                    VariableSending.controlnameP2.MakeCurrent();
                    controllerViberate();
                    Invoke("stopViberate", 0.2f);
                }

                if (levelManager.getP2Pinballs() < 10)
                {
                    p.setRespawnLocations(respawnLocs);
                    levelManager.incPlayerPinballs(-1, p.getSide());
                    p.setSide(!p.getSide());
                    levelManager.incPlayerPinballs(1, p.getSide());
                    p.resetPinball();
                }
                else
                {
                    levelManager.incPlayerPinballs(-1, p.getSide());
                    Destroy(other.gameObject);
                }
            }
            //P2's net zone
            else
            {
                if (VariableSending.p1GamePad)
                {
                    VariableSending.controlnameP1.MakeCurrent();
                    controllerViberate();
                    Invoke("stopViberate", 0.2f);
                }

                if (levelManager.getP1Pinballs() < 10)
                {
                    p.setRespawnLocations(respawnLocs);
                    levelManager.incPlayerPinballs(-1, p.getSide());
                    p.setSide(!p.getSide());
                    levelManager.incPlayerPinballs(1, p.getSide());
                    p.resetPinball();
                }
                else
                {
                    levelManager.incPlayerPinballs(-1, p.getSide());
                    Destroy(other.gameObject);
                }
            }

        }
    }

    private void controllerViberate()
    {
        InputSystem.pollingFrequency = 120;
        Gamepad.current.SetMotorSpeeds(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));
    }

    private void stopViberate()
    {
        InputSystem.ResetHaptics();
    }
}
