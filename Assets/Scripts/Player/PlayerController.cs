using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameOriginator;

public class PlayerController : MonoBehaviour, IOriginator
{
    private static PlayerController instance;
    private Transform cameraTransform;

    [Header("Player Settings")]
    public int defaultSpeed;
    public int Speed;
    public float jump;
    public int defaultJump;
    public float rotateSpeed;
    public float gravityScale;
    public Vector3 movement;

    [Header("Components")]
    public Animator anim;
    public CharacterController controller;

 

    //public Transform pivot;
    public GameObject playerModel;
    public Renderer head;
    public Renderer ears;
    public Renderer arms;
    public Renderer body;

    [Header("Knockback Settings")]
    public float knockbackForce;
    public float knockBackTime;
    private float knockBackTimer;
    public bool platform;

    [SerializeField] private int coinCount;
    [SerializeField] private int enemyCount;
    private GameOriginator originator;
    private GameCareTaker careTaker;
    private Vector3 startPosition;

    private void Awake()
    {
        //pivot = Camera.main.transform.GetChild(0).gameObject.transform;
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

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Speed = defaultSpeed;
        jump = defaultJump;
        cameraTransform = Camera.main.transform;

        originator = GetComponent<GameOriginator>();
        careTaker = new GameCareTaker();
        careTaker.SetOriginator(originator);
        startPosition = transform.position;
    }

    void Update()
    {
        if (knockBackTimer <= 0)
        {
            float Ystore = movement.y;
            

            Vector3 direction= new Vector3 (Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
            direction.Normalize();

            if(direction.magnitude>0)
            {
                Vector3 cameraForward = cameraTransform.forward;
                cameraForward.y = 0;
                cameraForward.Normalize();
                Quaternion cameraRotation = Quaternion.LookRotation(cameraForward);
                direction = cameraRotation * direction;

                movement = direction * Speed;
                movement.y = Ystore;

                Quaternion newRotation = Quaternion.LookRotation(movement);
                newRotation.x = playerModel.transform.rotation.x;
                newRotation.z= playerModel.transform.rotation.z;
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation,newRotation,rotateSpeed*Time.deltaTime);
            }
            else
            {
                movement.x = 0;
                movement.z = 0;

            }

            if (controller.isGrounded)
            {
                //movement.y = -gravityScale;
                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }
            }
            else if(platform)
            {
                //movement.y = -gravityScale;
                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }
            }
        }
        else
        {
            knockBackTimer -= Time.deltaTime;
        }

        movement.y = movement.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(movement * Time.deltaTime);
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + (Mathf.Abs(Input.GetAxis("Horizontal")))));

    }
    public void Jump()
    {
        AudioManager.Get().JumpSFX();
        movement.y = jump;
    }
    public void KnockBack(Vector3 Direction)
    {
        knockBackTimer = knockBackTime;
        movement = Direction * knockbackForce;
        movement.y = knockbackForce;
    }
    
    public static PlayerController Get()
    {
        return instance;
    }

    public void SetEnemyCount()
    {
        enemyCount++;
        SetEnemies();
        EnemyScoreSystem.Get().UpdateEnemyScore(enemyCount);
    }

    public void SetCoins()
    {

        coinCount = CoinManager.instance.GetCoins();
        originator.SetStateCoins(coinCount);
        //careTaker.SaveCheckPoint();
    }

    public void SetPosition()
    {
        originator.SetStatePlayerPosition(transform.position);
    }

    public void SetEnemies()
    {
        originator.SetStateEnemies(enemyCount);
    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawn");

        IMemento memento = careTaker.LoadLastCheckPoint();

        if (memento != null)
        {
            originator.Restore(memento);

            controller.enabled = false;
            transform.position = originator.GetPlayerPosition();
            controller.enabled = true;

            coinCount = originator.GetCoinCount();
            enemyCount = originator.GetEnemiesDefeated();

            EnemyScoreSystem.Get().UpdateEnemyScore(enemyCount);
            CoinManager.instance.CoinCollected(coinCount);
        }
        else
        {
            controller.enabled = false;
            transform.position = startPosition;
            controller.enabled = true;

            coinCount = 0;
            enemyCount = 0;

            EnemyScoreSystem.Get().UpdateEnemyScore(enemyCount);
            CoinManager.instance.CoinCollected(coinCount);
        }       
    }

    public IMemento Save()
    {
        //originator.SetStatePlayerPosition(transform.position);
        //originator.SetStateEnemies(enemyCount);
        //throw new System.NotImplementedException();
       return originator.Save();
    }

    public void Restore(IMemento memento)
    {
        throw new System.NotImplementedException();
    }

    public void SaveCheckPoint()
    {
        careTaker.SaveCheckPoint();
    }
}
