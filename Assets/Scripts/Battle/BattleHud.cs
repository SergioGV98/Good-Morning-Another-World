using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] HPBar hpBar;

    public void SetData(Enemy enemy)
    {
        nameText.text = enemy.Base.Name;
        levelText.text = "Lvl " + enemy.Level;
        hpBar.SetHP((float) enemy.HP / enemy.MaxHP);
    }
}
