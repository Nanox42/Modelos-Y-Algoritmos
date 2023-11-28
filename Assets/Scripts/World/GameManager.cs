using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float coins;
    public TextMeshProUGUI coinText;
    private static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddCoins(int ammount)
    {
        coins += ammount;
        coinText.text = ": " + coins;
    }

    public void MenuBackButton() 
    {
        Destroy(PlayerController.Get().gameObject);
        SceneManager.LoadScene("ChoosePlayer");
    }

    public static GameManager Get()
    {
        return instance;
    }
}
