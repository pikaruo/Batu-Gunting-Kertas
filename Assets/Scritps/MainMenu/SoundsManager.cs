using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundsManager : MonoBehaviour
{

    // * agar audio bisa di putar di berbagai scene
    public static SoundsManager Instance { get; set; }
    public AudioSource music;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // * untuk atur volume dengan slidder
    public Slider volumeSlider;
    public void SaveVolume()
    {
        Instance.music.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeSlider.value);
        LoadVolume();
        Debug.Log("Set Volume" + volumeSlider.value);
    }

    public void LoadVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        Debug.Log("VOLUME SET LOAD VALUE : " + volumeValue);
    }

    // * mute
    public Toggle toggle;
    public void MuteSound()
    {
        if (toggle.isOn == true)
        {
            music.mute = false;
            Debug.Log("Status Music Mute :" + music.mute);
        }
        else
        {
            music.mute = true;
            Debug.Log("Status Music Mute :" + music.mute);
        }
    }
}
