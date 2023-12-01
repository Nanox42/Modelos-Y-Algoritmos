using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOriginator : MonoBehaviour, IOriginator
{
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private int coinCount;
    [SerializeField] private int enemiesDefeated;

    public IMemento Save ()
    {
        return new GameMemento(playerPosition,coinCount,enemiesDefeated);
    }

    public void Restore(IMemento memento)
    {
        GameMemento castedMemento = (GameMemento)memento;
        playerPosition = castedMemento.playerPosition;
        coinCount = castedMemento.coinCount;
        enemiesDefeated = castedMemento.enemiesDefeated;
    }

    public void SetStatePlayerPosition(Vector3 position)
    {
        playerPosition = position;
    }
    public void SetStateCoins(int coins)
    {
        coinCount = coins;
    }
    public void SetStateEnemies(int enemies)
    {
        enemiesDefeated = enemies;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    public int GetEnemiesDefeated()
    {
        return enemiesDefeated;
    }

    [System.Serializable]
    private class GameMemento : IMemento
    {
        public Vector3 playerPosition; // se dejó public porque la clase es private
        public int coinCount; // se dejó public porque la clase es private
        public int enemiesDefeated; // se dejó public porque la clase es private

        public GameMemento(Vector3 position, int coins, int enemies)
        {
            this.playerPosition = position;
            this.coinCount = coins;
            this.enemiesDefeated = enemies;
        }

       
    }    
}

public interface IOriginator
{
    IMemento Save();
    void Restore(IMemento memento);
    //void SetStatePlayerPosition(Vector3 position);
    //void SetStateCoins(int coins);
    //void SetStateEnemies(int enemies);
}

public interface IMemento
{
}
