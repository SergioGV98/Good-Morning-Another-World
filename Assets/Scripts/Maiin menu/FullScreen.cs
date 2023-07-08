using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    [SerializeField] private Toggle fullscreenToggle;
    private string fullscreenKey = "FullScreen";

    private void Start()
    {
        LoadFullscreenState();
    }

    public void FullScreenON(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        SaveFullscreenState(fullscreen);
    }

    private void LoadFullscreenState()
    {
        if (PlayerPrefs.HasKey(fullscreenKey))
        {
            bool fullscreenState = PlayerPrefs.GetInt(fullscreenKey) == 1;
            fullscreenToggle.isOn = fullscreenState;
            Screen.fullScreen = fullscreenState;
        }
        else
        {
            fullscreenToggle.isOn = Screen.fullScreen;
        }
    }

    private void SaveFullscreenState(bool fullscreenState)
    {
        int value = fullscreenState ? 1 : 0;
        PlayerPrefs.SetInt(fullscreenKey, value);
        PlayerPrefs.Save();
    }

}
