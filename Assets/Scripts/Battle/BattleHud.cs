using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class BattleHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] HPBar hpBar;
    [SerializeField] ManaBar manaBar;


    public void SetData(object character)
    {
        if (character is Enemy)
        {
            Enemy enemy = (Enemy)character;
            nameText.text = enemy.Base.Name;
            levelText.text = "Lvl " + enemy.Level;
            hpBar.SetHP((float)enemy.HP / enemy.MaxHP);
        } else if (character is Player)
        {
            Player player = (Player)character;
            nameText.text = player.Base.Name;
            levelText.text = "Lvl " + player.Level;
            hpBar.SetHP((float)player.HP / player.MaxHP);
            manaBar.SetMana((float)player.mana / player.MaxMana);
        }
    }

    public void UpdateHP(object character)
    {
        if (character is Enemy)
        {
            Enemy enemy = (Enemy)character;
            hpBar.SetHP((float)enemy.HP / enemy.MaxHP);
        }
        else if (character is Player)
        {
            Player player = (Player)character;
            hpBar.SetHP((float)player.HP / player.MaxHP);
        }
    }

    public void UpdateMana(Player character)
    {
        manaBar.SetMana((float) character.mana / character.MaxMana);
    }
}
