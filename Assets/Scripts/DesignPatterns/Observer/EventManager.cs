using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
   private int coins;
   public static EventManager instance { get; private set; }
   [SerializeField] private CoinCollectedEvent onCoinCollected; //no debe ser expuesto y por eso es private
   
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

    //event manager lanza eventos
    public void CoinCollected(int currentCoins)
    {
        onCoinCollected?.Invoke(currentCoins);
        coins = currentCoins;
    }
    //event manager administra eventos
    public void SubscribeOnCoinCollected(UnityAction<int> callback)
    {
        onCoinCollected.AddListener(callback);
    }

    public void UnSubscribeOnCoinCollected(UnityAction<int> callback)
    {
        onCoinCollected.RemoveListener(callback);
    }

    public int GetCoins()
    {
        return coins;
    }
}
