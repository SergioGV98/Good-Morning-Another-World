using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{

    [SerializeField] PlayerUnit playerUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    private void Start()
    {
        SetupBattle();
    }

    public void SetupBattle()
    {
        playerUnit.Setup();
        playerHud.SetData(playerUnit.player);
        enemyUnit.Setup();
        enemyHud.SetData(enemyUnit.Enemy);

        StartCoroutine(dialogBox.TypeDialog($"Un { enemyUnit.Enemy.Base.Name} apareci�."));
    }
}
