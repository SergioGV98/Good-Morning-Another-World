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
                Debug.Log(currentMagicMove);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMagicMove > 0)
            {
                --currentMagicMove;
                Debug.Log(currentMagicMove);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMagicMove < playerUnit.player.Moves.Count -2)
            {
                currentMagicMove += 2; ;
                Debug.Log(currentMagicMove);
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currentMagicMove > 1)
            {
                currentMagicMove -= 2;
                Debug.Log(currentMagicMove);
            }
        }


        dialogBox.UpdateMagicSelection(currentMagicMove, playerUnit.player.Moves[currentMagicMove]);
    }
}
