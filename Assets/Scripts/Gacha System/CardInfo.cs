using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new card", menuName = "Character/Create new character")]

public class CardInfo : ScriptableObject
{
    public Sprite image;
    public new string name;
}
