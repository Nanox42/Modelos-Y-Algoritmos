using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPanel : Panel
{
    [SerializeField]
    private Panel instructionPanel;

    public void OpenInstructionPanel()
    {
        ScreenManager.Instance.Push(instructionPanel);
    }
}
