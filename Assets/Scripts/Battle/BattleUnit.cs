using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] EnemyBase _base;
    [SerializeField] PlayerUnit _playerUnit;
    [SerializeField] byte level;
    [SerializeField] bool isPlayerUnit;

    public Enemy Enemy { get; set; }
    public PlayerUnit PlayerUnit { get { return _playerUnit; } }

    Image image;
    Vector3 originalPos;

    public void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
    }

    public void Setup()
    {
        Enemy = new Enemy(_base, level);

        image.sprite = Enemy.Base.FrontSprite;

        PlayerUnitEnterAnimation();
        EnemyUnityEnterAnimation();
    }

    public void PlayerUnitEnterAnimation()
    {
        // Encuentra el objeto PlayerUnit en la escena
        PlayerUnit playerUnit = FindObjectOfType<PlayerUnit>();

        if (playerUnit != null)
        {
            Vector3 originalPos = playerUnit.transform.localPosition;
            // Mueve el objeto PlayerUnit a la posición deseada
            playerUnit.transform.localPosition = new Vector3(-500f, playerUnit.transform.localPosition.y);
            playerUnit.transform.DOLocalMoveX(originalPos.x, 1f);
        }
    }

    public void EnemyUnityEnterAnimation()
    {
        image.transform.localPosition = new Vector3(500f, originalPos.y);
        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }
}
