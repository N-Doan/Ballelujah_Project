using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemColor : MonoBehaviour

{


   
    public Material[] GemMaterials;
    ResourceManager essance; 
    // Start is called before the first frame update
    void Start()
    {
        essance = gameObject.GetComponent<ResourceManager>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        int value = essance.getPlayerEssence();
        
        FillColor(value);
    }
    private void setMaterials() { }
    private void FillColor(int val)
    {
        //Gem #1
        if (val == 10)
        {
            
            GemMaterials[0].SetFloat("_LerpValue", 1);
        }
        else
        {
           
            GemMaterials[0].SetFloat("_LerpValue", 0);
        }
        //Gem 2
        if (val == 9 || val > 9)
        {
           
            GemMaterials[1].SetFloat("_LerpValue", 1);
        }
        else
        {
           
            GemMaterials[1].SetFloat("_LerpValue", 0);
        }
        //Gem 3
        if (val == 8 || val > 8)
        {
            
            GemMaterials[2].SetFloat("_LerpValue", 1);
        }
        else
        {
           
            GemMaterials[2].SetFloat("_LerpValue", 0);
        }

        //Gem 4
        if (val == 7 || val > 7)
        {
            
            GemMaterials[3].SetFloat("_LerpValue", 1);
        }
        else
        {
            
            GemMaterials[3].SetFloat("_LerpValue", 0);
        }
        //Gem 5
        if (val == 6 || val > 6)
        {
            
            GemMaterials[4].SetFloat("_LerpValue", 1);
        }
        else
        {
            
            GemMaterials[4].SetFloat("_LerpValue", 0);
        }
        //Gem 6

        if (val == 5 || val > 5)
        {
           
            GemMaterials[5].SetFloat("_LerpValue", 1);
        }
        else
        {
           
            GemMaterials[5].SetFloat("_LerpValue", 0);
        }
        //Gem 7 
        if (val == 4 || val > 4)
        {
           
            GemMaterials[6].SetFloat("_LerpValue", 1);
        }
        else
        {
            
            GemMaterials[6].SetFloat("_LerpValue", 0);
        }
        //Gem 8
        if (val == 3 || val > 3)
        {
           
            GemMaterials[7].SetFloat("_LerpValue", 1);
        }
        else
        {
          
            GemMaterials[7].SetFloat("_LerpValue", 0);
        }
        //Gem 9 
        if (val == 2 || val > 2)
        {
            
            GemMaterials[8].SetFloat("_LerpValue", 1);
        }
        else
        {
            
            GemMaterials[8].SetFloat("_LerpValue", 0);
        }
        //Gem 10
        if (val == 1 || val > 1)
        {
         
            GemMaterials[9].SetFloat("_LerpValue", 1);
        }
        else
        {
           
            GemMaterials[9].SetFloat("_LerpValue", 0);
        }

    }
}
