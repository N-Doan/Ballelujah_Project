using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NomadAblilityDisplayCanvas : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    public TextMeshProUGUI title;
    [SerializeField]
    public TextMeshProUGUI desc;

    [SerializeField]
    private string fireNomadDesc;
    [SerializeField]
    private string airNomadDesc;
    [SerializeField]
    private string waterNomadDesc;
    [SerializeField]
    private string earthNomadDesc;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setFireText() 
    {
        setTitle("FIRE NOMAD", fireNomadDesc);
    }
    public void setEarthText()
    {
        setTitle("EARTH NOMAD", earthNomadDesc);
    } 
    public void setAirText() 
    {
        setTitle("AIR NOMAD", airNomadDesc);
    } 
    public void setWaterText() 
    {
        setTitle("WATER NOMAD", waterNomadDesc);
    }

    private void setTitle(string titletext, string desctext) 
    {
        title.text = titletext;
        desc.text = desctext;
    }


}
