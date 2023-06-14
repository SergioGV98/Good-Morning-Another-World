using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Create new enemy")]

public class EnemyBase : ScriptableObject
{
    [SerializeField] private new string name;

    public string Name { get { return name; } }

    [SerializeField] private string description;

    public string Description { get { return description; } }

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
    Normal,
    Fire,
    Water,
    Electric
}
