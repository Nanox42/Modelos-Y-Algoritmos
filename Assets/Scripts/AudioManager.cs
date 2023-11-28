using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource JumpSource;
    [SerializeField] List<AudioClip> jumpClips = new List<AudioClip>();
    [SerializeField] AudioSource CoinSource;
    [SerializeField] List<AudioClip> coinClips = new List<AudioClip>();
    [SerializeField] AudioSource PowerUpSource;
    [SerializeField] List<AudioClip> powerClips = new List<AudioClip>();
    private static AudioManager instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void JumpSFX()
    {
        AudioClip clip = jumpClips[Random.Range(0, jumpClips.Count)];
        JumpSource.PlayOneShot(clip);
    }
    public void CoinSFX()
    {
        AudioClip clip = coinClips[Random.Range(0, coinClips.Count)];
        CoinSource.PlayOneShot(clip);
    }
    public void PowerUpSFX()
    {
        AudioClip clip = powerClips[Random.Range(0, powerClips.Count)];
        PowerUpSource.PlayOneShot(clip);
    }

    public static AudioManager Get()
    {
        return instance;
    }
}
