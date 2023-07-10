using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Slider sliderBGM;
    [SerializeField] private Image imagenMuteBGM;
    [SerializeField] private Image imagenMuteSFX;
    [SerializeField] private Image imagenLowVolumeSFX;
    [SerializeField] private Image imagenNormalVolumeSFX;
    [SerializeField] private Image imagenLowVolumeBGM;
    [SerializeField] private Image imagenNormalVolumeBGM;

    private void Awake()
    {
        ChangueImagenVolume();
    }

    public void SetVolumeSFX()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sliderSFX.value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sliderSFX.value);
        PlayerPrefs.Save();
        ChangueImagenVolume();
    }

    public void SetVolumeBGM()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(sliderBGM.value) * 20);
        PlayerPrefs.SetFloat("BGMVolume", sliderBGM.value);
        PlayerPrefs.Save();
        ChangueImagenVolume();
    }

    public void ChangueImagenVolume()
    {
        audioMixer.GetFloat("SFX", out float volumeSFX);
        audioMixer.GetFloat("BGM", out float volumeBGM);

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
