using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CannonTowerAi : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField]
    private Animator animControl;

    [Header("Pinball Spawner")]
    [Tooltip("SO WE DOING A LITTLE TROLLING NOW")]
    public GameObject pinballSpawner;

    [Header("Fire Location")]
    [SerializeField]
    public Transform shotPos;

    [Header("Fire Power")]
    [Tooltip("I WAS ADDING THIS AND THEN I WAS LIKE EFFORT, NO ONE GONNA SEE IT ANYWAY")]
    [SerializeField]
    public float power;

    [Header("Cooldown Between Shots")]
    [Tooltip("The value changes the time inbetween shots for the Cannnon Tower, Time in Seconds. Note changing the value of the " +
        "cooldown time affects the cooldown time for the Cannon Tower Upgrade")]
    [SerializeField]
    public float cooldownTime;
    [Header("Spawnable Object")]
    [Tooltip("The Spawnable Pinball for the duplicatied Fire,")]
    [SerializeField]
    private GameObject spawnPb;

    [Header("Materials")]
    public MeshRenderer mesh;
    public Material currentMaterial;

    [Header("VFX")]
    public VisualEffect cannonFire;

    [Header("Booleans")]
   
    public bool canFire;
    public bool fired;
    public bool dupFired;
    public bool fullyUpgraded;

    private GivePointsCannonTower pointScript;

    // Start is called before the first frame update
    void Start()
    {
        cooldownTime = 4f;
        canFire = true;
        fired = false;
        dupFired = false;
        fullyUpgraded = false;
        currentMaterial = this.GetComponent<Renderer>().material;
        pinballSpawner = FindObjectOfType<PinballSpawner>().gameObject;
        animControl = this.GetComponent<Animator>();
        pointScript = GetComponent<GivePointsCannonTower>();

    }

    // Update is called once per frame
    void Update()
    {
  

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pinball" && GetComponent<CannonTowerAi>().enabled)
        {
            Rigidbody pinballRb = collision.rigidbody;
            fire(pinballRb);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Pinball" && GetComponent<CannonTowerAi>().enabled) 
        {
            Rigidbody pinballRb = collision.rigidbody;
            fire(pinballRb);
        }
    }

    private void fire(Rigidbody pinballRb) 
    {
        if (canFire == true)
        {
            pointScript.givePoints();
            animControl.SetTrigger("CannonFire");
            cannonFire.Play();
            pinballRb.MovePosition(shotPos.position);
            pinballRb.AddForce(shotPos.forward * power, ForceMode.Impulse);
            FMODUnity.RuntimeManager.PlayOneShot("event:/explosion");
            EventManager.instance.OnTowerTriggerEnter(gameObject.GetInstanceID());
            fired = true;
            canFire = false;
            StartCoroutine(coolDown());
        }
        if (fullyUpgraded == true) {
            dupliFire(pinballRb.gameObject);
        }
    }

    public void dupliFire(GameObject pinball)
    {
        
        if (dupFired == false)
        {
            cannonFire.Play();
            GameObject ball = GameObject.Instantiate(spawnPb);
            ball.transform.parent = pinballSpawner.transform;
            ball.transform.position = shotPos.position;
            ball.GetComponent<Pinball>().setSide(pinball.GetComponent<Pinball>().getSide());

            ball.GetComponent<Pinball>().setRespawnLocations(pinball.GetComponent<Pinball>().getRespawnLocations());
  
            Rigidbody otherbody = ball.GetComponent<Rigidbody>();
      
            otherbody.AddForce(shotPos.forward * power, ForceMode.Impulse);
            dupFired = true;
        
            StartCoroutine(cooldownDup());
        }
        else {

    
        }
    }

   

    IEnumerator coolDown() 
    {
        yield return new WaitForSeconds(cooldownTime);
        canFire = true;
        fired = false;
    }

    IEnumerator cooldownDup()
    {
        yield return new WaitForSeconds(30f);
        dupFired = false;
    }
}
