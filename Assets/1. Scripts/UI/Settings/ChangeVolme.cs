using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ChangeVolume : MonoBehaviour
{
    [SerializeField] AudioMixer _myMixer;

    [SerializeField] Slider _masterSlider, _musicSlider, _sfxSlider;
    [SerializeField] Toggle _masterToggle, _musicToggle, _sfxToggle;

    private bool _isOnMaster = true;
    private bool _isOnMusic = true;
    private bool _isOnSFX = true;

    private float _masterVolume;
    private float _musicVolume;
    private float _sfxVolume;


    private void Awake()
    {
        _masterSlider.value = PlayerPrefs.GetFloat("Master", 0.5f);
        _musicSlider.value = PlayerPrefs.GetFloat("Music", 0.5f);
        _sfxSlider.value = PlayerPrefs.GetFloat("SFX", 0.5f);

        Debug.Log(PlayerPrefs.GetFloat("Master"));
        Debug.Log(PlayerPrefs.GetFloat("Music"));
        Debug.Log(PlayerPrefs.GetFloat("SFX"));
    }

    private void Start()
    {
        OnSetMasterVolume();
        OnSetMusicVolume();
        OnSetSFXVolume();
    }


    public void OnSetMasterVolume()
    {
        float volume = Mathf.Clamp(_masterSlider.value, 0.001f, 1.0f);
        _masterVolume = volume;
        _myMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }


    public void OnSetMusicVolume()
    {
        float volume = Mathf.Clamp(_musicSlider.value, 0.001f, 1.0f);
        _musicVolume = volume;
        _myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }


    public void OnSetSFXVolume()
    {
        float volume = Mathf.Clamp(_sfxSlider.value, 0.001f, 1.0f);
        _sfxVolume = volume;
        _myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }


    public void OnMasterToggle()
    {
        if (_isOnMaster)
        {
            _isOnMaster = false;

            SetState(false);

            _myMixer.SetFloat("Master", Mathf.Log10(0.001f) * 20);
        }
        else
        {
            _isOnMaster = true;

            SetState(true);

            OnSetMasterVolume();

            if (!_isOnMusic)
            {
                _isOnMusic = true;
                OnMusicToggle();
                _musicToggle.isOn = false;
            }

            if (!_isOnSFX)
            {
                _isOnSFX = true;
                OnSFXToggle();
                _sfxToggle.isOn = false;
            }
        }
    }


    public void OnMusicToggle()
    {
        if (_isOnMusic)
        {
            _isOnMusic = false;
            _musicSlider.interactable = false;
            _myMixer.SetFloat("Music", Mathf.Log10(0.001f) * 20);
        }
        else
        {
            _isOnMusic = true;
            _musicSlider.interactable = true;
            OnSetMusicVolume();
        }
    }


    public void OnSFXToggle()
    {
        if (_isOnSFX)
        {
            _isOnSFX = false;
            _sfxSlider.interactable = false;
            _myMixer.SetFloat("SFX", Mathf.Log10(0.001f) * 20);
        }
        else
        {
            _isOnSFX = true;
            _sfxSlider.interactable = true;
            OnSetSFXVolume();
        }
    }


    public void SetState(bool p_Flag)
    {
        _masterSlider.interactable = p_Flag;
        _musicSlider.interactable = p_Flag;
        _sfxSlider.interactable = p_Flag;

        _musicToggle.interactable = p_Flag;
        _sfxToggle.interactable = p_Flag;
    }


    private void OnDisable()
    {
        PlayerPrefs.SetFloat("Master", _masterVolume);
        PlayerPrefs.SetFloat("Music", _musicVolume);
        PlayerPrefs.SetFloat("SFX", _sfxVolume);
        PlayerPrefs.Save();
    }
}