using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
   

    private void Start()
    {
        EventManager.instance.SubscribeOnCoinCollected(UpdateScore);
    }

    private void OnDestroy()
    {
        EventManager.instance.UnSubscribeOnCoinCollected(UpdateScore);
    }

    private void UpdateScore(int currentCoins)
    {
        scoreText.text = "coins: " + currentCoins;
    }

}