using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InstructionPanel : Panel
{
    [SerializeField]
    private Panel next;

    public void OpenNext()
    {
        ScreenManager.Instance.Push(next);
    }
}
