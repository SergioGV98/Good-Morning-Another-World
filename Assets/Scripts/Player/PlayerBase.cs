using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player/Create new player")]

public class PlayerBase : ScriptableObject
{
    [SerializeField] private new string name;

    public string Name { get { return name; } }

    [SerializeField] private Sprite frontSprite;

    public Sprite FrontSprite { get { return frontSprite; } }

    [SerializeField] private int maxHp;

    public int MaxHp { get { return maxHp; } }

    [SerializeField] private int maxMana;

    public int MaxMana { get {  return maxMana; } }

    [SerializeField] private short attack;

    public short Attack { get { return attack; } }

    [SerializeField] private short defense;

    public short Defense { get { return defense; } }

    [SerializeField] private short speed;

    public short Speed { get { return speed; } }

    [SerializeField] List<LearnableMove> learnableMoves;

    public List<LearnableMove> LearnableMoves { get { return learnableMoves; } }

}
