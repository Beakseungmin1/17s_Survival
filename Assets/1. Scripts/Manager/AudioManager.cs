using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound<T>
{
    public T name;
    public AudioClip clip;
}

public class AudioManager : ConvertSingleton<AudioManager>
{
    [SerializeField] private AudioSource _musicPlayer = null;
    [SerializeField] private AudioSource[] _sfxPlayer = null;

    [Header("-------BGM-------")]
    [SerializeField] private Sound<Music>[] _music = null;

    [Header("-------SFX-------")]
    [SerializeField] private Sound<PlayerSFX>[] _playerSFX = null;
    [SerializeField] private Sound<EnemySFX>[] _enemySFX = null;
    [SerializeField] private Sound<EmbientSFX>[] _embientSFX = null;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(PlayerSFX playerSFX)
    {
        for (int i = 0; i < _playerSFX.Length; i++)
        {
            if (playerSFX.Equals(_playerSFX[i].name))
            {
                for (int j = 0; j < _sfxPlayer.Length; j++)
                {
                    if (!_sfxPlayer[j].isPlaying)
                    {
                        _sfxPlayer[j].clip = _playerSFX[i].clip;
                        _sfxPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("every player is working");
                return;
            }
        }
        Debug.Log("There is no name called" + playerSFX);
        return;
    }


    public void PlaySFX(EnemySFX enemySFX)
    {
        for (int i = 0; i < _enemySFX.Length; i++)
        {
            if (enemySFX.Equals(_enemySFX[i].name))
            {
                for (int j = 0; j < _sfxPlayer.Length; j++)
                {
                    if (!_sfxPlayer[j].isPlaying)
                    {
                        _sfxPlayer[j].clip = _enemySFX[i].clip;
                        _sfxPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("every player is working");
                return;
            }
        }
        Debug.Log("There is no name called" + enemySFX);
        return;
    }


    public void PlaySFX(EmbientSFX embientSFX)
    {
        for (int i = 0; i < _embientSFX.Length; i++)
        {
            if (embientSFX.Equals(_embientSFX[i].name))
            {
                for (int j = 0; j < _sfxPlayer.Length; j++)
                {
                    if (!_sfxPlayer[j].isPlaying)
                    {
                        _sfxPlayer[j].clip = _embientSFX[i].clip;
                        _sfxPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("every player is working");
                return;
            }
        }
        Debug.Log("There is no name called" + embientSFX);
        return;

    }


    public void PlayMusic(Music music)
    {
        for (int i = 0; i < _music.Length; i++)
        {
            if (music.Equals(_music[i].name))
            {
                _musicPlayer.clip = _music[i].clip;
                _musicPlayer.Play();
                return;
            }
        }
        Debug.Log("There is no name called" + music);
    }
}