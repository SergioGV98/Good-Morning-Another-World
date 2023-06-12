using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cards : MonoBehaviour
{

    public CardInfo card;

    [SerializeField] private Image img;
    [SerializeField] private new TextMeshProUGUI name;


    void Start()
    {
        if(card != null)
        {
            img.sprite = card.image;
            name.text = card.name;
        }
    }

   
    void Update()
    {
        Start();
    }
}
