using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

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
        dialogBox.SetBattleSystem(this);

        dialogBox.SetMagicMovesNames(playerUnit.player.Moves);

        yield return dialogBox.TypeDialog($"Un { enemyUnit.Enemy.Base.Name} apareció.");
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
        enemyUnit.PlayPlayerAttackAnimation();
        yield return new WaitForSeconds(0.5f);
        enemyUnit.EnemyPlayHitAnimation();
        var damageDetails = enemyUnit.Enemy.TakeDamage(magic, playerUnit.player);
        bool isOutMana = playerUnit.player.UpdateMana(magic);
        yield return enemyHud.UpdateHP(enemyUnit.Enemy);
        yield return playerHud.UpdateMana(playerUnit.player);
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Enemy.Base.Name} fue derrotado.");
            enemyUnit.PlayEnemyFaintedAnimation();
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
        enemyUnit.EnemyPlayerAttackAnimation();
        yield return new WaitForSeconds(0.5f);
        enemyUnit.PlayerPlayHitAnimation();
        var damageDetails = playerUnit.player.TakeDamage(move, enemyUnit.Enemy);
        yield return playerHud.UpdateHP(playerUnit.player);
        yield return ShowDamageDetailsPlayer(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.player.Base.Name} fuiste derrotado.");
            enemyUnit.PlayPlayerFaintedAnimation();
        }
        else
        {
            PlayerAction();
        }
    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        if(damageDetails.Critical > 1f)
        {
            yield return dialogBox.TypeDialog("¡Golpe critico!");
        }

        if(damageDetails.Type > 1f)
        {
            yield return dialogBox.TypeDialog("¡El golpe hace tambalearse al rival!");
        } else if (damageDetails.Type < 1f)
        {
            yield return dialogBox.TypeDialog("El golpe parece que no afecta al rival.");
        }
    }

    IEnumerator ShowDamageDetailsPlayer(DamageDetails damageDetails)
    {
        if (damageDetails.Critical > 1f)
        {
            yield return dialogBox.TypeDialog("¡Golpe critico!");
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

    public Boolean HaveMana(Move magic)
    {
        if (playerUnit.player.mana >= magic.mana)
        {
            return true;
        }
        return false;
    }

}
