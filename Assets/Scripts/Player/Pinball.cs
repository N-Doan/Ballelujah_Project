using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Pinball : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField]
    private Transform[] respawnLocs;
    [SerializeField]
    private float respawnTime;

    //Which side of the board the pinball is on
    [SerializeField]
    private bool side;

    bool isRespawning = false;

    [SerializeField]
    private float maxSpeed = 1.75f;

    int instanceID;

    [SerializeField]
    GameObject flipperHitVfx;

    private bool recentlyHit = false;

    public bool getSide()
    {
        return side;
    }
    public void setSide(bool newSide)
    {
        side = newSide;
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        instanceID = transform.parent.gameObject.GetInstanceID();
        EventManager.instance.EOnPinballCollision += pinballCollide;
    }

    private void FixedUpdate()
    {
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * 1.75f * Time.deltaTime;
        }
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isRespawning && collision.relativeVelocity.magnitude >= 1.5f)
        {
            if(collision.gameObject.CompareTag("Terrain")|| collision.gameObject.CompareTag("Flipper") || collision.gameObject.CompareTag("Tower"))
            {
                int holder = Random.Range(0, 2);
                if (holder == 0) FMODUnity.RuntimeManager.PlayOneShot("event:/Wall and floor hit");
                else FMODUnity.RuntimeManager.PlayOneShot("event:/pinballCollide");
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        EventManager.instance.OnPinballHit(instanceID);
        if (collision.gameObject.layer == 8 && !recentlyHit)
        {
            if (collision.gameObject.GetComponent<flipperScript>().isUp == true && collision.relativeVelocity.magnitude >= 0.9f)
            {
                //flipperHitVfx.Play();
                GameObject flipVFX = Instantiate(flipperHitVfx);
                flipVFX.transform.position = transform.position;
                Destroy(flipVFX, 1.5f);
                recentlyHit = true;
                StartCoroutine(hitCooldown());
            }
        }
    }
    public void resetPinball()
    {
        //zero out velocities
        //move to respawn location
        gameObject.GetComponent<Renderer>().enabled = false;
        StartCoroutine(Reactivate());
    }

    public void setRespawnLocations(Transform[] newLocs)
    {
        respawnLocs = newLocs;
    }
    public Transform[] getRespawnLocations()
    {
        return respawnLocs;
    }
    IEnumerator Reactivate()
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnTime);
        //re-enable renderer and set position to respawn position
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.transform.position = respawnLocs[Random.Range(0, respawnLocs.Length)].position;
        isRespawning = false;
        yield return null;
    }

    IEnumerator hitCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        recentlyHit = false;
        StopCoroutine(hitCooldown());
    }

    //could add a rigid body force check so it doesn't trigger as often (if force greater than #)
    private void pinballCollide(int id)
    {
        if (id == instanceID)
        {
            //Debug.Log("BALL HAS COLLIDED WITH OBJECT");

        }
    }
}
