using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Moves/Create new move")]

public class MoveBase : ScriptableObject
{
    [SerializeField] private new string name;

    public string Name
    {
        get { return name; }
    }

    [SerializeField] private string description;

    public string Description
    {
        get { return description; }
    }

    [SerializeField] private EnemyType type;

    public EnemyType Type
    {
        get { return type; }
    }

    [SerializeField] private short power;

    public short Power
    {
        get { return power; }
    }

    [SerializeField] private byte accuracy;

    public byte Accuracy
    {
        get { return accuracy; }
    }

    [SerializeField] private short mana;

    public short Mana
    {
        get { return mana; }
    }


}
