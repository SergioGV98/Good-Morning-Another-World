using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : MonoBehaviour
{
    [SerializeField] PlayerBase _base;
    [SerializeField] byte level;
    [SerializeField] bool isPlayerUnit;

    public Player player { get; set; }

    public void Setup()
    {
        player = new Player(_base, level);

        if (isPlayerUnit)
        {
            GetComponent<Image>().sprite = player.Base.FrontSprite;
        }
    }
}
