using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HowToPlayText : MonoBehaviour
{
    public bool backOFF = false;
    public bool nextOFF = false;
    public bool runonce = false;

    [SerializeField]
    public GameObject NextPage;
    [SerializeField]
    public GameObject BackPage;
    [SerializeField]
    public GameObject[] texts;
    public string[] titleTexts;
    public Image[] imagePages;

    public int pageCount;
    [SerializeField]
    public TextMeshProUGUI title;
    EventSystem eventSystem;

    public GameObject backButton;

    int count;

    private Button button;
    private void Start()
    {
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(backButton);
        pageCount = 0;
        runonce = false;
        displayText();
        button = BackPage.GetComponent<Button>();

    }


    public void howToPlayStartUp() 
    {
        for (int i = 0; i < texts.Length; i++) 
        {
            texts[i].SetActive(false);
            
        }
        for (int i = 0; i < imagePages.Length; i++) 
        {
            imagePages[i].gameObject.SetActive(false);
        }
       
    }

    

    private void Update()
    {

        interactableButtons();
        updateTitle();
       
        if (eventSystem.currentSelectedGameObject == null && nextOFF == true) 
        {
            setButton(BackPage);
        }

        if (eventSystem.currentSelectedGameObject == null && backOFF == true )
        {
            setButton(NextPage);
        }


    }

    public void pageForward() 
    {
        pageCount = pageCount + 1;
        displayText();
       
    }

    public void pageBack() 
    {
        pageCount = pageCount - 1;
        displayText();
    }

    public void displayText()
    {

         howToPlayStartUp();
         texts[pageCount].SetActive(true);
         updateImages();
    }

    private void updateTitle() 
    {
        
        if (pageCount == 0) 
        { 
            title.text = titleTexts[0];
        }
        else if (pageCount == 1)
        {
            title.text = titleTexts[1];
        }
        else if (pageCount == 2)
        {
            title.text = titleTexts[2];
        }
        else if (pageCount == 3)
        {
            title.text = titleTexts[3];
        }
        else if (pageCount == 4)
        {
            title.text = titleTexts[4]; 
        }
        else if (pageCount == 5)
        {
            title.text = titleTexts[5];
        }
        else if (pageCount == 6)
        {
            title.text = titleTexts[6];
        }
        else if (pageCount == 7)
        {
            title.text = titleTexts[7];
    
        }
        else if (pageCount == 8)
        { 
            title.text = titleTexts[8];
        }
        else if (pageCount == 9)
        {
            title.text = titleTexts[9];
        }
        else 
        {
            title.text = " ";
        }
    }

    private void updateImages() 
    {
        if (pageCount == 0)
        {
            imagePages[0].gameObject.SetActive(true);
        }
        if (pageCount == 4)
        {
            imagePages[1].gameObject.SetActive(true);
        }
        if (pageCount == 5)
        {
            imagePages[2].gameObject.SetActive(true);
        }
        if (pageCount == 7)
        {
            imagePages[3].gameObject.SetActive(true);
        }
        if (pageCount == 8)
        {
            imagePages[4].gameObject.SetActive(true);
        }

    }

    private void interactableButtons() 
    {
        //int counter = 0;
        if (pageCount == texts.Length - 1)
        {
            NextPage.GetComponent<Button>().interactable = false;
            //setButton(BackPage,counter);
            nextOFF = true;
           // counter = 1;
        }
        else
        {
            NextPage.GetComponent<Button>().interactable = true;
            nextOFF = false;
          
            //counter = 0;
        }

        if (pageCount == 0)
        {
            //BackPage.SetActive(false);
            BackPage.GetComponent<Button>().interactable = false;                           
          //  setButton(NextPage, counter);
            backOFF = true;
            runonce = false;
        }
        else
        {
            BackPage.GetComponent<Button>().interactable = true;
      
            backOFF = false;
            
          //  counter = 0;
        }
    }

    private void setButton(GameObject button) 
    {
       
      eventSystem.SetSelectedGameObject(button);
           
        
        
    }
}
