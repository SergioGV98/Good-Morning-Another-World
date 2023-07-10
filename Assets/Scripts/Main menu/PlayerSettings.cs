using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider SFX;
    [SerializeField] private Slider BGM;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolumeSFX();
        }else
        {
            audioMixer.SetFloat("SFX", 0);
        }

        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            LoadVolumeBGM();
        } else
        {
            audioMixer.SetFloat("BGM", 0);
        }
    }

    public void LoadVolumeSFX()
    {
        SFX.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        audioMixer.SetFloat("SFX", Mathf.Log10(SFX.value) * 20);
        float cargado = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
    }

    public void LoadVolumeBGM()
    {
        BGM.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        audioMixer.SetFloat("BGM", Mathf.Log10(BGM.value) * 20);
        float cargado = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
    }

}
