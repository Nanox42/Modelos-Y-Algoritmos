using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOriginator : MonoBehaviour
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
        playerPosition = ((GameMemento)memento).playerPosition;
        coinCount = ((GameMemento)memento).coinCount;
        enemiesDefeated = ((GameMemento)memento).enemiesDefeated;
    }

    public void SetState(Vector3 position, int coins, int enemies)
    {
        playerPosition = position;
        coinCount = coins;
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
        public Vector3 playerPosition;
        public int coinCount;
        public int enemiesDefeated;

        public GameMemento(Vector3 position, int coins, int enemies)
        {
            this.playerPosition = position;
            this.coinCount = coins;
            this.enemiesDefeated = enemies;
        }

        public void LoadTo(GameOriginator originator)
        {
            originator.playerPosition = playerPosition;
            originator.coinCount = coinCount;
            originator.enemiesDefeated = enemiesDefeated;
        }
    }    
}

public interface IMemento
{
    void LoadTo(GameOriginator originator);
}
