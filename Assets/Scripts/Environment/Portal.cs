using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script gets attached to both player's portals
public class Portal : MonoBehaviour
{
    //the exit point of the portal (use an empty game object slightly below the portal itself so balls don't get stuck inside
    [SerializeField]
    private Transform exitPoint;
    [SerializeField]
    bool storeVelocity;
    //TRUE = P1 FALSE = P2
    [SerializeField]
    bool side;
    //use the other player's respawn locations here
    [SerializeField]
    private Transform[] respawnLocs;

    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    GameObject portalHitVfx;

    int instanceID;

    void Start()
    {
        instanceID = transform.parent.gameObject.GetInstanceID();
        EventManager.instance.EOnPortalTriggered += ballInPortal;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Pinball")
        {
            //triggering the portal event
            GameObject flipVFX = Instantiate(portalHitVfx);
            flipVFX.transform.position = new Vector3(0.9337f, -0.0249f, collision.gameObject.transform.position.z);
            Destroy(flipVFX, 1.5f);

            //Debug.Log("PORTAL HIT");
            //zeroing out the pinball's velocity OR reversing it depending on the boolean value
            if (storeVelocity)
            {
                Vector3 temp = collision.gameObject.GetComponent<Rigidbody>().velocity;
                collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(temp.x * -1, temp.y * -1, temp.z * -1);
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                collision.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            }
            collision.gameObject.GetComponent<Pinball>().setRespawnLocations(respawnLocs);
            //-1 pinball from p1, +1 to p2

            //setting the pinball that collided with the portal's transform equal to the exit point + this portal's transform minus the pinball's current transform
            collision.gameObject.transform.position = exitPoint.position + (gameObject.transform.position - collision.gameObject.transform.position);
            if (side)
            {
                //updating the pinball's side to the P2's side
                collision.gameObject.GetComponent<Pinball>().setSide(false);

                levelManager.incPlayerPinballs(-1, true);
                //destroy the pinball if it triggers their reservoir
                if (levelManager.incPlayerPinballs(1, false) == false)
                {
                    Destroy(collision.gameObject);
                }

            }
            //-1 pinball from p2, +1 to p1
            else
            {
                //updating the pinball's side to the P1's side
                collision.gameObject.GetComponent<Pinball>().setSide(true);

                levelManager.incPlayerPinballs(-1, false);
                //destroy the pinball if it triggers their reservoir
                if(levelManager.incPlayerPinballs(1, true) == false)
                {
                    Destroy(collision.gameObject);
                }

            }
            EventManager.instance.OnPortalTriggerEnter(transform.parent.gameObject.GetInstanceID());
        }
    }

    //Triggers once a pinball enters the portal (put particles and sounds in here)
    private void ballInPortal(int id)
    {
        if (id == instanceID)
        {
            //Debug.Log("BALL HAS ENTERED PORTAL ZONE");
            //GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
