using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    EnemyBase _base;
    byte level;

    public Enemy(EnemyBase pbase, byte plevel)
    {
        _base = pbase;
        level = plevel;
    }

    public int MaxHP
    {
        get
        {
            return Mathf.FloorToInt((_base.MaxHp * level) / 100f) + 10;
        }
    }

    public short Attack
    {
        get 
        { 
            return (short)(Mathf.FloorToInt((_base.Attack * level) / 100f) + 5); 
        }
    }

    public short Defense
    {
        get
        {
            return (short)(Mathf.FloorToInt((_base.Defense * level) / 100f) + 5);
        }
    }

    public short Speed
    {
        get
        {
            return (short)(Mathf.FloorToInt((_base.Speed * level) / 100f) + 5);
        }
    }
}
