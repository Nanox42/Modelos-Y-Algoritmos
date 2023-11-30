using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPanel : Panel
{
    [SerializeField]
    private Panel instructionPanel;

    [SerializeField]
    private Panel ChoosePlayer;

    public void OpenInstructionPanel()
    {
        ScreenManager.Instance.Push(instructionPanel);
    }

    public void OpenChoosePlayer()
    {
        ScreenManager.Instance.Push(ChoosePlayer);
    }
}
