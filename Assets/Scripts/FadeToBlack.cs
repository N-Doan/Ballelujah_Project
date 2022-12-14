using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public UiController ui;
    public sceneLoad scenelOAD;
    [Header("Animation Controller")]
    public Animator anim;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fading(int i) 
    {
        StartCoroutine(Fading(i));
    }
    IEnumerator Fading(int i)
    {
       
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => image.color.a == 1);
        if (i == 0)
        {
            GameChange();
        }
        else if (i == 1)
        {
            TutChange();
        }
    }
    private void GameChange()
    {
        //SceneManager.LoadScene(1);
        StartCoroutine(scenelOAD.loadAsyncLevel(1)); 
    }
    private void TutChange()
    {
        //SceneManager.LoadScene(1);
        StartCoroutine(scenelOAD.loadAsyncLevel(2));
    }
}
