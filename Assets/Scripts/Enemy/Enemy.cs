using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public EnemyBase Base { get; set; }
    public byte Level { get; set; }

    public int HP { get; set; }

    public List<Move> Moves { get; set; }

    public int MaxHP
    {
        get
        {
            return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10;
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

    public Enemy(EnemyBase pbase, byte plevel)
    {
        Base = pbase;
        Level = plevel;
        HP = MaxHP;

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

    public bool TakeDamage(Move move, Player attacker)
    {
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);
        HP -= damage;

        if(HP <= 0)
        {
            HP = 0;
            return true;
        } else
        {
            return false;
        }
      
    }

    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }
}

