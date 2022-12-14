using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{

    FMOD.Studio.Bus sound;
    FMOD.Studio.Bus audio;
    private void Start()
    {
        audio = FMODUnity.RuntimeManager.GetBus("bus:/BGM_Main");
        sound = FMODUnity.RuntimeManager.GetBus("bus:/tester");
    }

    public void muteSound()
    {
        sound.setPaused(true);
    }

    public void resumeSound()
    {
        sound.setPaused(false);
    }
    public void muteAudio()
    {
        audio.setPaused(true);
    }

    public void resumeAudio()
    {
        audio.setPaused(false);
    }
}
