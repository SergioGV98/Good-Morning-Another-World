
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    [SerializeField] private Toggle fullscreenToggle;
    private string fullscreenKey = "FullScreen";
    public TMP_Dropdown resolucionesDropDown;
    public RefreshRate refreshRate;
    Resolution[] resoluciones;

    private void Start()
    {
        LoadFullscreenState();
        RevisarResolucion();
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

    public void RevisarResolucion()
    {
        resoluciones = Screen.resolutions;
        resolucionesDropDown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;
        refreshRate = Screen.currentResolution.refreshRateRatio;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string _opciones = resoluciones[i].width + " x " + resoluciones[i].height + "  " +  refreshRate + " Hz";
            opciones.Add(_opciones);

            if(Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }


        resolucionesDropDown.AddOptions(opciones);
        resolucionesDropDown.value = resolucionActual;
        resolucionesDropDown.RefreshShownValue();

        resolucionesDropDown.value = PlayerPrefs.GetInt("resolucionGuardada", 0);
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        indiceResolucion = resolucionesDropDown.value;
        PlayerPrefs.SetInt("resolucionGuardada", indiceResolucion);
        Resolution resolution = resoluciones[indiceResolucion];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Has cambiado la resolucion a " + resolution);
    }

}
