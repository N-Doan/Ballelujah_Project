using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsPage : MonoBehaviour
{
    public GameObject[] pages;

    // Start is called before the first frame update
    void Start()
    {

        arraySetActive(pages,0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void creditsNextButton() 
    {
        arraySetActiveFalse(pages, 0);
        arraySetActive(pages, 1);
        
    }
    public void creditsBackButton() 
    {
        arraySetActiveFalse(pages, 1);
        arraySetActive(pages, 0);
    }

    private void arraySetActive(GameObject[] text, int pageCount) 
    {
        
        text[pageCount].SetActive(true);
        
    }

    private void arraySetActiveFalse(GameObject[] text, int pageCount)
    {
      
        text[pageCount].SetActive(false);
        
    }
}
