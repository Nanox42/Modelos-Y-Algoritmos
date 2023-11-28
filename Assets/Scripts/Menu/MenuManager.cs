using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartButton()
    {
        if(ChoosePlayer.currentPlayer!=0)
        {
            SceneManager.LoadScene("Level TP");
        }
        else
        {
            Debug.Log("choose your player");
        }
    }
}
