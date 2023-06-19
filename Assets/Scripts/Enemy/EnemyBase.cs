using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Create new enemy")]

public class EnemyBase : ScriptableObject
{
    [SerializeField] private new string name;

    public string Name { get { return name; } }

    [SerializeField] private Sprite frontSprite;

    public Sprite FrontSprite { get { return frontSprite; } }

    [SerializeField] private EnemyType type;

    public EnemyType Type { get { return type; } }

    [SerializeField] private int maxHp;

    public int MaxHp { get { return maxHp; } }

    [SerializeField] private short attack;

    public short Attack { get { return attack; } }

    [SerializeField] private short defense;

    public short Defense { get { return defense; } }

    [SerializeField] private short speed;

    public short Speed { get { return speed; } }

    [SerializeField] List<LearnableMove> learnableMoves;

    public List<LearnableMove> LearnableMoves { get { return learnableMoves; } }


}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase move;
    public MoveBase Base { get { return move; } }
    [SerializeField] byte level;
    public short Level { get { return level; } }    
}

public enum EnemyType
{
    Energ�a,
    Caos,
    Esencia,
    Tecnolog�a,
    Cosmos,
    Ilusi�n,
    Naturaleza
}


public class TypeChart
{
    static float[][] chart = {
        //                           Energia | Caos | Esencia | Tecnologia | Cosmos | Ilusion | Naturaleza
        /* Energia */   new float[] {   1f,     2f,      2f,        0.5f,     0.5f,    0.5f,      2f },
        /* Caos */      new float[] {   0.5f,   1f,      0.5f,      2f,       2f,      2f,        0.5f },
        /* Esencia */   new float[] {   2f,     0.5f,    1f,        2f,       0.5f,    2f,        2f },
        /* Tecnologia */new float[] {   2f,     2f,      2f,        1f,       0.5f,    0.5f,      2f },
        /* Cosmos */    new float[] {   2f,     2f,      0.5f,      2f,       1f,      0.5f,      2f },
        /* Ilusion */   new float[] {   0.5f,   2f,      2f,        0.5f,     0.5f,    1f,        2f },
        /* Naturaleza */new float[] {   2f,     0.5f,    2f,        2f,       2f,      2f,         1f }
    };

    public static float GetEffectiveness(EnemyType attackType, EnemyType defenseType)
    {
        if (attackType == EnemyType.Energ�a || defenseType == EnemyType.Energ�a)
        {
            return 1;
        }
        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;

        return chart[row][col];
    }
}
