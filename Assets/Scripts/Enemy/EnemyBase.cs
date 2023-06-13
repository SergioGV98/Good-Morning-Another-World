using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Create new enemy")]

public class EnemyBase : ScriptableObject
{
    [SerializeField] new string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] EnemyType type;

    // Base Stats
    [SerializeField] int maxHp;
    [SerializeField] short attack;
    [SerializeField] short defense;
    [SerializeField] short speed;

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
    }

    public EnemyType Type
    {
        get { return type; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }

    public short Attack
    {
        get { return attack; }
    }

    public short Defense
    {
        get { return defense; }
    }

    public short Speed
    {
        get { return speed; }
    }

}

public enum EnemyType
{
    Normal,
    Fire,
    Water,
    Electric
}
