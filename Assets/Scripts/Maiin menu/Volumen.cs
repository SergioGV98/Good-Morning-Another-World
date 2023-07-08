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
    [SerializeField] private Image imagenMuteBGM;
    [SerializeField] private Image imagenMuteSFX;
    [SerializeField] private Image imagenLowVolumeSFX;
    [SerializeField] private Image imagenNormalVolumeSFX;
    [SerializeField] private Image imagenLowVolumeBGM;
    [SerializeField] private Image imagenNormalVolumeBGM;

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
        EstoyMuted();
    }

    public void SetSFXVolume()
    {
        float volumeSFX = sliderSFX.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volumeSFX) * 20);

        PlayerPrefs.SetFloat("SFX", volumeSFX);
        EstoyMuted();
    }

    public void SetBGMVolume()
    {
        float volumeBGM = sliderBGM.value;
        myMixer.SetFloat("BGM", Mathf.Log10(volumeBGM) * 20);

        PlayerPrefs.SetFloat("BGM", volumeBGM);
        EstoyMuted();
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
        myMixer.GetFloat("SFX", out float volumeSFX);
        myMixer.GetFloat("BGM", out float volumeBGM);

        imagenMuteSFX.enabled = false;
        imagenLowVolumeSFX.enabled = false;
        imagenNormalVolumeSFX.enabled = false;

        if (volumeSFX <= -80f)
        {
            imagenMuteSFX.enabled = true;
        }
        else if (volumeSFX <= -6f)
        {
            imagenLowVolumeSFX.enabled = true;
        }
        else
        {
            imagenNormalVolumeSFX.enabled = true;
        }

        imagenMuteBGM.enabled = false;
        imagenLowVolumeBGM.enabled = false;
        imagenNormalVolumeBGM.enabled = false;

        if (volumeBGM <= -80f)
        {
            imagenMuteBGM.enabled = true;
        }
        else if (volumeBGM <= -6f)
        {
            imagenLowVolumeBGM.enabled = true;
        }
        else
        {
            imagenNormalVolumeBGM.enabled = true;
        }
    }


}
