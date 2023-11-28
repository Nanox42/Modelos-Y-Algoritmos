using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PlayerController pc;
    [SerializeField] private Renderer head;
    [SerializeField] private Renderer ears;
    [SerializeField] private Renderer arms;
    [SerializeField] private Renderer body;
    [SerializeField] private int fadeTime;
    [SerializeField] private Color headColor;
    [SerializeField] private Color earsColor;
    [SerializeField] private Color armsColor;
    [SerializeField] private Color bodyColor;

    private static PowerUpManager instance;

    void Awake()
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

    public void speedPowerUp(int ammount)
    {
        AudioManager.Get().PowerUpSFX();
        NormalizeStats();
        Debug.Log("Powered up");
        pc.Speed *= ammount;
        head.material.color = new Color(1, 1, 0);
        ears.material.color = new Color(1, 1, 0);
        body.material.color = new Color(1, 1, 0);
        arms.material.color = new Color(1, 1, 0);
        StartCoroutine(PowerFade());
    }
    public void jumpPowerUp(float ammount)
    {
        AudioManager.Get().PowerUpSFX();
        NormalizeStats();
        Debug.Log("Powered up");
        pc.jump *= ammount;
        head.material.color = new Color(1, 0, 0);
        ears.material.color = new Color(1, 0, 0);
        body.material.color = new Color(1, 0, 0);
        arms.material.color = new Color(1, 0, 0);
        StartCoroutine(PowerFade());
    }
    public void inmortalPowerUp()
    {
        AudioManager.Get().PowerUpSFX();
        NormalizeStats();
        Debug.Log("Powered up");
        HealthSystem.Get().inmortal = true;
        head.material.color = new Color(0, 0, 0);
        ears.material.color = new Color(0, 0, 0);
        body.material.color = new Color(0, 0, 0);
        arms.material.color = new Color(0, 0, 0);
        StartCoroutine(PowerFade());
    }
    public IEnumerator PowerFade()
    {
        yield return new WaitForSeconds(fadeTime);

        NormalizeStats();
    }
    public void NormalizeStats()
    {
        head.material.color = headColor;
        ears.material.color = earsColor;
        body.material.color = bodyColor;
        arms.material.color = armsColor;
        pc.Speed = pc.defaultSpeed;
        pc.jump = pc.defaultJump;
        HealthSystem.Get().inmortal = false;
    }

    public static PowerUpManager Get()
    {
        return instance;
    }
}
