using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
   

    void Start()
    {
        RevisarResolucion();
    }

    public void RevisarResolucion()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string _opciones = resolutions[i].width + " x " + resolutions[i].height;
            opciones.Add(_opciones);

            resolucionActual = i;
        }


        resolutionDropdown.AddOptions(opciones);
        resolutionDropdown.value = resolucionActual;
        resolutionDropdown.RefreshShownValue();
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        Resolution resolution = resolutions[indiceResolucion];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

}