using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro; // Tambahkan namespace TextMesh Pro

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _sfxSlider; // Tambahkan Slider untuk volume SFX
    [SerializeField] private TMP_Text _bgmVolumeText;
    [SerializeField] private TMP_Text _masterVolumeText;
    [SerializeField] private TMP_Text _sfxVolumeText; // Tambahkan Text untuk volume SFX

    private void Start()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            LoadBGMVolume();
        }
        else
        {
            _bgmSlider.value = 0.5f;  // Atur nilai default ke 0.5
            SetBGMVolume();
        }

        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            LoadMasterVolume();
        }
        else
        {
            _masterSlider.value = 0.5f;  // Atur nilai default ke 0.5
            SetMasterVolume();
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadSFXVolume();
        }
        else
        {
            _sfxSlider.value = 0.5f;  // Atur nilai default ke 0.5
            SetSFXVolume();
        }
    }

    public void SetBGMVolume()
    {
        float volume = _bgmSlider.value;

        if (volume <= 0.0001f)
        {
            _audioMixer.SetFloat("BGM", -80f);
        }
        else
        {
            _audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        }

        PlayerPrefs.SetFloat("BGMVolume", volume);
        _bgmVolumeText.text = (volume * 100).ToString("0") + "%";
    }

    public void SetMasterVolume()
    {
        float volume = _masterSlider.value;

        if (volume <= 0.0001f)
        {
            _audioMixer.SetFloat("Master", -80f);
        }
        else
        {
            _audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        }

        PlayerPrefs.SetFloat("MasterVolume", volume);
        _masterVolumeText.text = (volume * 100).ToString("0") + "%";
    }

    public void SetSFXVolume()
    {
        float volume = _sfxSlider.value;

        if (volume <= 0.0001f)
        {
            _audioMixer.SetFloat("SFX", -80f);
        }
        else
        {
            _audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        }

        PlayerPrefs.SetFloat("SFXVolume", volume);
        _sfxVolumeText.text = (volume * 100).ToString("0") + "%";
    }

    private void LoadBGMVolume()
    {
        _bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        SetBGMVolume();
    }

    private void LoadMasterVolume()
    {
        _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        SetMasterVolume();
    }

    private void LoadSFXVolume()
    {
        _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetSFXVolume();
    }
}
