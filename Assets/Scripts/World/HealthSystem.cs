using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    private static HealthSystem instance;
    [Header("Health Stats")]
    public float maxHealth;
    public float currentHealth;

    [Header("Components")]
    public Image blackScreen;
    //public Renderer playerHead;
    //public Renderer playerEars;
    //public Renderer playerBody;
    //public Renderer playerArms;

    [Header("Invincibility Settings")]
    public float invincibilityLength;
    public float invincibilityTimer;
    public bool inmortal;

    [Header("Flash Settings")]
    public float flashCounter;
    public float flashLength = 0.1f;

    [Header("Respawn Settings")]
    public float fadeSpeed;
    public float waitForFade;
    public float respawnLength;
    private bool isRespawning;
    private Vector3 respawnPoint;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public InitializerPlayer initializerPlayer;

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
    public static HealthSystem Get()
    {
        return instance;
    }
    public float Health
    {
        get { return currentHealth; }
        set
        {
            if (value > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth = value;
            }
        }
    }

    void Start()
    {
        Health = maxHealth;
        respawnPoint = initializerPlayer.activePlayer.transform.position;
    }

    private void Update()
    {

        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                //playerHead.enabled = !playerHead.enabled;
                //playerEars.enabled = !playerEars.enabled;
                //playerBody.enabled = !playerBody.enabled;
                //playerArms.enabled = !playerArms.enabled;
                flashCounter = flashLength;
            }
            if(invincibilityTimer <= 0)
            {
                //playerHead.enabled = true;
                //playerEars.enabled = true;
                //playerBody.enabled = true;
                //playerArms.enabled = true;
            }
        }
        if(isFadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }
        if (isFadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }
    }

    public void takeDamage(float ammount, Vector3 direction)
    {
        if (!inmortal)
        {
            if (invincibilityTimer <= 0)
            {
                Health -= ammount;
                if (Health <= 0)
                {
                    Respawn();
                }
                else
                {
                    PlayerController.Get().KnockBack(direction);
                    invincibilityTimer = invincibilityLength;
                    //playerHead.enabled = false;
                    //playerEars.enabled = false;
                    //playerBody.enabled = false;
                    //playerArms.enabled = false;

                    flashCounter = flashLength;
                }

            }
        }

        
    }
    public void Respawn()
    {
        if(!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
        
    }
    public IEnumerator RespawnCo()
    {
        Debug.Log("respawn");
        isRespawning = true;
        //PlayerController.Get().gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnLength);
        //isFadeToBlack = true;
        //yield return new WaitForSeconds(waitForFade);
        PlayerController.Get().RespawnPlayer();
        //isFadeToBlack = false;
        //isFadeFromBlack = true;
        isRespawning = false;

        /*PlayerController.Get().gameObject.SetActive(true);
        
        CharacterController charController = PlayerController.Get().GetComponent<CharacterController>();

        charController.enabled = false;
        Destroy(PlayerController.Get().gameObject);
        charController.enabled = true;
        currentHealth = maxHealth;

        invincibilityTimer = invincibilityLength;
        flashCounter = flashLength;
        GetComponent<SceneSwitcher>().ReloadLevel();*/


    }
    public void healDamage(float ammount)
    {
        Health += ammount;
    }
}
