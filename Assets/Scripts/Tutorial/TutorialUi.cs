using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialUi : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI welcome;

    [SerializeField]
    private string[] tutPrompt;
    [SerializeField]
    private GameObject[] images;
    [SerializeField]
    private TextMeshProUGUI[] desc;



    public void titleShow() 
    {
        welcome.gameObject.SetActive(true);  
    }
    public void titleHide()
    {
        welcome.gameObject.SetActive(false);
    }

    public void setTutorialPromptActive(bool doYouWantActive, int promptNum) 
    {
     
        if(doYouWantActive)
        {
            setTutorialPrompt(promptNum);
            images[promptNum].SetActive(true);
            
            
        }
        else 
        {
            images[promptNum].SetActive(false);
        }

    }

    private void setTutorialPrompt(int promptNumber) 
    {
        desc[promptNumber].text = tutPrompt[promptNumber];
    }
    

}
