using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move 
{
    public MoveBase Base {  get; set; }

    public short mana { get; set; }

    public Move(MoveBase pBase, short pMana)
    {
        Base = pBase;
        mana = pMana;
    }
}
