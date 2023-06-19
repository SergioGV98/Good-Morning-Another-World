using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

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
            return Mathf.FloorToInt(Base.MaxMana);
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

    public DamageDetails TakeDamage(Move move, Enemy attacker)
    {
        float critical = 1f;
        if (Random.value * 100f < 5.25f)
        {
            critical = 2f;
        }

        var damageDetails = new DamageDetails()
        {
            Critical = critical,
            Fainted = false
        };

        float modifiers = Random.Range(0.85f, 1f) * critical;
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            damageDetails.Fainted = true;
        }
        else
        {
            damageDetails.Fainted = false;
        }

        return damageDetails;

    }

    public bool UpdateMana(Move move)
    {
        mana -= move.Base.Mana;
        if(mana <= 0)
        {
            mana = 0;
            return true;
        } else
        {
            return false;
        }
    }
}
