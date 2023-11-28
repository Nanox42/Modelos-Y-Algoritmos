using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCareTaker : MonoBehaviour
{
    private IMemento currentCheckPointState;
    private GameOriginator originator;

    public GameCareTaker() {}

    public void SetOriginator(GameOriginator originator)
    {
        this.originator = originator;
    }

    public void SaveCheckPoint()
    {
        if (originator != null)
        {
            currentCheckPointState = originator.Save();
        }
    }

    public IMemento LoadLastCheckPoint()
    {
        return currentCheckPointState;
    }
}
