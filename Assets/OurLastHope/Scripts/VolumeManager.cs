using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _slider;
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            LoadBGMVolume();
        }
        else
        {
            SetBGMVolume();    
        }
    }

    public void SetBGMVolume()
    {
        float volume = _slider.value;
        _audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume",volume);
    }

    private void LoadBGMVolume()
    {
        _slider.value = PlayerPrefs.GetFloat("BGFMVolume");
        SetBGMVolume();
    }
}
