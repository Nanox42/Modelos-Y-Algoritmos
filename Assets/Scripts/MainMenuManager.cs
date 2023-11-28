using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject tutorial;
  
    void Update()
    {
        if(Input.GetKey("q"))
        {
            tutorial.SetActive(false);
            menu.SetActive(true);
        }
    }
    public void OpenTutorial()
    {
        menu.SetActive(false);
        tutorial.SetActive(true);
    }

    public void CloseTutorial()
    {
        tutorial.SetActive(false);
        menu.SetActive(true);
    }
}
