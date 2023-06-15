using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    public PlayerBase Base { get; set; }
    public byte Level { get; set; }

    public int HP { get; set; }
    public int mana { get; set; }

    public List<Move> Moves { get; set; }

    public int MaxHP
    {
        get
        {
            return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10;
        }
    }

    public int MaxMana
    {
        get
        {
            return Mathf.FloorToInt(Base.MaxMana * Level) + 10;
        }
    }

    public short Attack
    {
        get
        {
            return (short)(Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5);
        }
    }

    public short Defense
    {
        get
        {
            return (short)(Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5);
        }
    }

    public short Speed
    {
        get
        {
            return (short)(Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5);
        }
    }

    public Player(PlayerBase pbase, byte plevel)
    {
        Base = pbase;
        Level = plevel;
        HP = MaxHP;
        mana = MaxMana;

        // Genera los movimientos
        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
            {
                Moves.Add(new Move(move.Base));
            }
            if (Moves.Count >= 10)

                break;
        }
    }
}
