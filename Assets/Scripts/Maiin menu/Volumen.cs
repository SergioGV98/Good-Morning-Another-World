using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Slider sliderBGM;
    public Image imagenMute;

    private void Start()
    {
        if (PlayerPrefs.HasKey("SFX"))
        {
            LoadSFXVolumen();
        }
        else
        {
            SetSFXVolume();
        }

        if (PlayerPrefs.HasKey("BGM"))
        {
            LoadBGMVolume();
        }
        else
        {
            SetBGMVolume();
        }
    }

    public void SetSFXVolume()
    {
        float volumeSFX = sliderSFX.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volumeSFX) * 20);

        PlayerPrefs.SetFloat("SFX", volumeSFX);
    }

    public void SetBGMVolume()
    {
        float volumeBGM = sliderBGM.value;
        myMixer.SetFloat("BGM", Mathf.Log10(volumeBGM) * 20);

        PlayerPrefs.SetFloat("BGM", volumeBGM);
    }

    private void LoadSFXVolumen()
    {
        sliderSFX.value = PlayerPrefs.GetFloat("SFX");
        SetSFXVolume();
    }

    private void LoadBGMVolume()
    {
        sliderBGM.value = PlayerPrefs.GetFloat("BGM");
        SetBGMVolume();
    }

    public void EstoyMuted()
    {
        if (sliderSFX.value == 0)
        {
            imagenMute.enabled = true;
        }
        else
        {
            imagenMute.enabled = false;
        }
    }
}
