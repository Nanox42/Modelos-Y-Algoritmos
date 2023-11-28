using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyScoreSystem : MonoBehaviour
{
    public static EnemyScoreSystem instance;
    public TextMeshProUGUI scoreText;

    private void Awake()
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
    public static EnemyScoreSystem Get()
    {
        return instance;
    }

    public void UpdateEnemyScore(int currentEnemies)
    {
        scoreText.text = "Enemies: " + currentEnemies;
    }

}