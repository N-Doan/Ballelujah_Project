using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoPlay : MonoBehaviour
{

    int currentlevel;
    [Header("Video")]
    VideoPlayer video;
    [Header("Animation Controller")]
    public Animator anim;
    public Image image;

    [Header("Skip Text")]
    [SerializeField]
    private Animator textAnim;
    private Text skipText;

    // Start is called before the first frame update
    void Awake()
    {
        currentlevel = SceneManager.GetActiveScene().buildIndex;
        video = GetComponent<VideoPlayer>();
        video.Play();
        StartCoroutine(DelaySkipText());
        video.loopPointReached += CheckOver;


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Joystick1Button0) )
        {
            StartCoroutine(Fading());
        }


    }

    private void CheckOver(VideoPlayer vp)
    {
        StartCoroutine(Fading());
    }

    private void sceneChange() 
    {
        SceneManager.LoadScene(currentlevel + 1);
    }
    IEnumerator Fading() 
    {
        textAnim.SetBool("FadeText", true);
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => image.color.a == 1);
        sceneChange();
    }

    IEnumerator DelaySkipText() 
    {
       
        yield return new WaitForSeconds(4f);
        textAnim.SetBool("FadeTextIn", true);
        //textAnim.Play("SkipText",0);
    }

}