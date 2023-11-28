using UnityEngine;

public abstract class Panel : MonoBehaviour
{
    public void GoBack()
    {
        ScreenManager.Instance.Pop();
    }

    private Panel cachedPrototype;

    public virtual Panel OnEnter()
    {
        //return Instantiate(this, ScreenManager.Instance.transform);
        if (cachedPrototype == null)
        {
            cachedPrototype = Instantiate(this, ScreenManager.Instance.transform);
        }
        return cachedPrototype;
    }

    public virtual void OnHide()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnShow()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnExit()
    {
        OnHide();
        //Destroy(gameObject);
    }
}
