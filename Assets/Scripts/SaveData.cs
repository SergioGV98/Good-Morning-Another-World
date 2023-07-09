using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SaveData : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        LoadSoundVolume();
    }

    private void LoadSoundVolume()
    {
        float BGMS = PlayerPrefs.GetFloat("BGM");
        Debug.Log(BGMS);
        audioMixer.SetFloat("BGM", BGMS);

        float SFXS = PlayerPrefs.GetFloat("SFX");
        audioMixer.SetFloat("SFX", SFXS);
    }
}
