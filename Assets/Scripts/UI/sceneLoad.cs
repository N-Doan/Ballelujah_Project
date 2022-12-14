using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class sceneLoad : MonoBehaviour
{


    public string levelName;
    //public Slider slider;



    // Update is called once per frame
    void Update()
    {
   
    }

    public IEnumerator loadAsyncLevel(int index) 
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        while (!operation.isDone) 
        {
            //slider.gameObject.SetActive(true);
            float progress = Mathf.Clamp01(operation.progress / 0.9f);   
            //slider.value = progress;
            yield return null;
           
        }
       
    }

 
}
