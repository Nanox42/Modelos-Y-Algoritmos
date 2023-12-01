using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class CoinManager : MonoBehaviour //, IOriginator
{
   private int coins;
   private GameOriginator originator; 
   public static CoinManager instance { get; private set; }
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
        originator.SetStateCoins(coins);
        return coins;
    }

    //public IMemento Save()
    //{
    //    return originator.SetStateCoins(coins);
    //}

    //public void Restore(IMemento memento)
    //{
    //    throw new NotImplementedException();
    //}
}
