using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    public void applyChange(Material m)
    {
        for(int i =0; i< GetComponentsInChildren<MeshRenderer>().Length;i++)
        {
            GetComponentsInChildren<MeshRenderer>()[i].material = m;
        }
    }
}
