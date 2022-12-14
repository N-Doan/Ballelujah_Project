using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    public LevelManager level;
    [SerializeField]
    public UIManager ui;
    [SerializeField]
    public GameObject firstButton;
    public GameObject sfx;
    public GameObject sound;
    public GameObject exitgame;
    public Sprite soundOn;
    public Sprite soundOff;
    public Sprite sfxOn;
    public Sprite sfxOff;

    public Button music;
    public Button sfxbutton;

    public bool SoundMuted;
    public bool SfxMuted;

    EventSystem eventSystem;
    private GameObject audio;

    private bool isAudio;
    private bool isSound;
    
    void Start()
    {
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(firstButton);
        audio = GameObject.FindGameObjectWithTag("MainAudio");
        isAudio = true;
        isSound = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(firstButton);
            Debug.Log("isNull");
        }
    }
    // UI CODING FOR PAUSE MENU 
    public void muteAUdio()
    {
        if(isAudio)
        {
            audio.GetComponent<AudioControl>().muteAudio();
            music.image.sprite = soundOff;
            isAudio = false;
        }
        else
        {
            audio.GetComponent<AudioControl>().resumeAudio();
            isAudio = true;
            music.image.sprite = soundOn;
        }
    }

    public void MuteSFX()
    {
        if(isSound)
        {
            audio.GetComponent<AudioControl>().muteSound();
            isSound = false;
            sfxbutton.image.sprite = sfxOff;
        }
        else
        {
            audio.GetComponent<AudioControl>().resumeSound();
            isSound = true;
            sfxbutton.image.sprite = sfxOn;
        }
    }
    public void returnToMainMenu()
    {
        InputSystem.ResetHaptics();
        ui.backToMain();
    }
    public void exitGame()
    {
        InputSystem.ResetHaptics();
        Application.Quit();
    }

    public void selectSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/button select");
    }

    public void backSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Back select");
    }

}
