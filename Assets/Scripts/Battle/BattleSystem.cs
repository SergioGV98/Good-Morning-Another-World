using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    Start,
    PlayerAction,
    PlayerMove,
    EnemyMove,
    Busy
}

public class BattleSystem : MonoBehaviour
{

    [SerializeField] PlayerUnit playerUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    BattleState state;
    byte currentAction;
    byte currentMagicMove;

    private void Start()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        playerHud.SetData(playerUnit.player);
        enemyUnit.Setup();
        enemyHud.SetData(enemyUnit.Enemy);

        dialogBox.SetMagicMovesNames(playerUnit.player.Moves);

        yield return dialogBox.TypeDialog($"Un { enemyUnit.Enemy.Base.Name} apareció.");

        yield return new WaitForSeconds(0.35f);

        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Elige una acción"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerMoveMagic()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMagicSelector(true);
    }

    IEnumerator PerformPlayerMagic()
    {
        state = BattleState.Busy;
        var magic = playerUnit.player.Moves[currentMagicMove];
        yield return dialogBox.TypeDialog($"{playerUnit.player.Base.Name} uso {magic.Base.name}");
        yield return new WaitForSeconds(1f);
        bool isFainted = enemyUnit.Enemy.TakeDamage(magic, playerUnit.player);
        bool isOutMana = playerUnit.player.UpdateMana(magic);
        enemyHud.UpdateHP(enemyUnit.Enemy);
        playerHud.UpdateMana(playerUnit.player);

        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Enemy.Base.Name} fue derrotado.");
        } else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        var move = enemyUnit.Enemy.GetRandomMove();
        yield return dialogBox.TypeDialog($"{enemyUnit.Enemy.Base.Name} uso {move.Base.Name}");

        yield return new WaitForSeconds(1f);
        bool isFainted = playerUnit.player.TakeDamage(move, enemyUnit.Enemy);
        playerHud.UpdateHP(playerUnit.player);

        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.player.Base.Name} fuiste derrotado.");
        }
        else
        {
            PlayerAction();
        }
    }

    private void Update()
    {
        if(state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        } else if (state == BattleState.PlayerMove)
        {
            HandleMagicSelection();
        }
    }

    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentAction < 2)
            {
                ++currentAction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentAction > 0)
            {
                --currentAction;
            }
        }

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentAction == 0)
            {
                // Golpear
            }
            else if (currentAction == 1)
            {
                // Magia
                PlayerMoveMagic();
            }
            else if (currentAction == 3)
            {
                // Huir
            }
        }
    }

    void HandleMagicSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMagicMove < playerUnit.player.Moves.Count -1)
            {
                ++currentMagicMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMagicMove > 0)
            {
                --currentMagicMove;
            }
        }

        dialogBox.UpdateMagicSelection(currentMagicMove, playerUnit.player.Moves[currentMagicMove]);

        // En un futuro comprobar si tienes mana para esa habilidad.

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogBox.EnableMagicSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMagic());
        }
    }
}
