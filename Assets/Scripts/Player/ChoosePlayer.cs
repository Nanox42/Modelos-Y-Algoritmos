using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePlayer : MonoBehaviour
{
    public static int currentPlayer;
    [SerializeField] private AudioClip audioButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image checkImage1;
    [SerializeField] private Image checkImage2;

    private void Start()
    {
       
        checkImage1.enabled = false;
        checkImage2.enabled = false;
    }
    public void ChoosePlayerButton (int a)
    {
        currentPlayer = a;
        audioSource.PlayOneShot(audioButton);
        if(a==1)
        {
            
            checkImage1.enabled = true;
            checkImage2.enabled = false;
        }
        if (a == 2)
        {
            
            checkImage1.enabled = false;
            checkImage2.enabled = true;
        }
    }
    
    
    
   
}
