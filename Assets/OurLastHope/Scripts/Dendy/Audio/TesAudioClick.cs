using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesAudioClick : MonoBehaviour {

    [SerializeField] private AudioSource ClickAudio;
    [SerializeField] private AudioSource CloseAudio;
    [SerializeField] private AudioSource CollectAudio;
    [SerializeField] private AudioSource SelectAudio;
    [SerializeField] private AudioSource UseAudio;

    public void ButtonClickAudio() {
        ClickAudio.Play();
    }

    public void ButtonCloseAudio() {
        CloseAudio.Play();
    }

    public void CollectItemAudio() {
        CollectAudio.Play();
    }

    public void SelectItemAudio() {
        SelectAudio.Play();
    }

    public void UseItemAudio() {
        UseAudio.Play();
    }

}
