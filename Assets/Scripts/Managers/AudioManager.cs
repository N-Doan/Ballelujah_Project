using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private FMODUnity.StudioEventEmitter backGroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseAudio()
    {
        var instance = backGroundMusic.EventInstance;
        instance.setPaused(true);
        instance.setVolume(0.25f);
    }
}
