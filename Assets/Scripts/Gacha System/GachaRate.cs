using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum R { Legend, Epic, Rare, Uncommon, Common}

[System.Serializable]  

public class GachaRate
{
    public string rarity;
    public R _rarity;

    [Range(1, 100)]

    public int rate;

    public CardInfo[] reward;
}
