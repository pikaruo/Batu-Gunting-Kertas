using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    // TODO Slider
    public Slider volumeSlider;

    // TODO berganti scene
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Debug.Log("Ini adalah Scene ke-" + sceneIndex);
    }

    private void Awake()
    {
        // TODO Mute
        if (SoundsManager.Instance.music.mute == true)
        {
            toggle.isOn = false;
            Debug.Log("Status Music Mute:" + SoundsManager.Instance.music.mute);
        }
        else
        {
            toggle.isOn = true;
            Debug.Log("Status Music Mute :" + SoundsManager.Instance.music.mute);
        }

    }

    // TODO resolution
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;

    int currentResolutionIndex = 0;

    void Start()
    {
        // TODO resolution
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // TODO Load Volume
        volumeSlider.value = SoundsManager.Instance.music.volume;
        SoundsManager.Instance.LoadVolume();
        Debug.Log("Volume Music : " + volumeSlider.value);

        // TODO fullscreen
        // if (Screen.fullScreen == Screen.fullScreen)
        // {
        //     toggle.isOn = true;
        // }
        // else
        // {
        //     toggle.isOn = false;
        // }

        // TODO Mute
        // if (SoundsManager.Instance.music.mute == true)
        // {
        //     toggle.isOn = false;
        //     Debug.Log("Status Music Mute:" + SoundsManager.Instance.music.mute);
        // }
        // else
        // {
        //     toggle.isOn = true;
        //     Debug.Log("Status Music Mute :" + SoundsManager.Instance.music.mute);
        // }
    }

    // TODO resolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Resolution : " + resolution);
    }

    // TODO Quality Resolution
    // public void SetQuality(int qualityIndex)
    // {
    //     QualitySettings.SetQualityLevel(qualityIndex);
    // }

    // TODO Fullscreen
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Fullscreen status : " + Screen.fullScreen);
    }

    // TODO slider volume
    public void SliderVolume()
    {
        SoundsManager.Instance.music.volume = volumeSlider.value;
        Debug.Log("Volume Music : " + volumeSlider.value);
    }

    // TODO mute
    public Toggle toggle;
    public void SetMute()
    {
        if (toggle.isOn == true)
        {
            SoundsManager.Instance.music.mute = false;
            Debug.Log("Status Music Mute :" + SoundsManager.Instance.music.mute);
        }
        else
        {
            SoundsManager.Instance.music.mute = true;
            Debug.Log("Status Music Mute :" + SoundsManager.Instance.music.mute);
        }
    }

    // TODO keluar game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Keluar Aplikasi");
    }
}
