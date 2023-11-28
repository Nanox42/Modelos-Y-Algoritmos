using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    private readonly Stack<Panel> panels = new Stack<Panel>();

    [SerializeField]
    private Panel initialPanel;

    [SerializeField]
    private UnityEvent onPopLastPanel;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singlenton can only have one instance");
            Destroy(this);
            return;
        }
        Instance = this;

        if (initialPanel != null)
        {
            Push(initialPanel);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pop();
        }
    }

    public void Push(Panel panelPrefab)
    {
        if (panels.TryPeek(out Panel previousPanel))
        {
            previousPanel.OnHide();
        }

        Panel newPanel = panelPrefab.OnEnter();
        newPanel.OnShow();

        panels.Push(newPanel);
    }

    public void Pop()
    {
        if (panels.TryPop(out Panel oldPanel))
        {
            oldPanel.OnExit();

            if (panels.TryPeek(out Panel newPanel))
            {
                newPanel.OnShow();
            }
            else
            {
                onPopLastPanel.Invoke();
            }
        }
    }
}
