using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBox : MonoBehaviour
{
    [SerializeField]
    float pushForce;

    public float getPushForce()
    {
        return pushForce;
    }

    public void setPushForce(float p)
    {
        pushForce = p;
    }

    //Which side of the board the tower is on
    [SerializeField]
    private bool side;

    public bool getSide()
    {
        return side;
    }
    public bool setSide()
    {
        return side;
    }

    private void Start()
    {

        side = GetComponentInParent<TowerBuildSpot>().getSide();
    }

    //apply small force to pinball on matching side
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Pinball") && other.gameObject.GetComponent<Pinball>().getSide() == side && gameObject.GetComponent<WindBox>().enabled)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/windTower");

            Rigidbody r = other.GetComponent<Rigidbody>();
            r.AddForce(this.transform.rotation * Vector3.forward * pushForce, ForceMode.VelocityChange);

            //apply wind tower effect if this tower is fully upgraded and it isn't already applied to the pinball in question
            if (gameObject.GetComponent<WindTowerUpgrader>().isFullyUpgraded() == true && other.gameObject.GetComponent<PinballSpecialEffectsController>().windTowerEffectActive == false)
            {
                other.gameObject.GetComponent<PinballSpecialEffectsController>().changePinballState(PinballSpecialEffectsController.Effects.WINDTOWER);
            }

            EventManager.instance.OnTowerTriggerEnter(gameObject.GetInstanceID());
        }


    }

}
