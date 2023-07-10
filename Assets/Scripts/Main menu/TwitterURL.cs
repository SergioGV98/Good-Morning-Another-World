using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitterURL : MonoBehaviour
{
    public void OpenURL()
    {
        string url = "https://twitter.com/SergioGV98";
        Application.OpenURL(url);
    } 
    
}
