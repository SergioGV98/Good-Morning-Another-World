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
    Color originalColor;

    public void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    public void Setup()
    {
        Enemy = new Enemy(_base, level);

        image.sprite = Enemy.Base.FrontSprite;

        image.color = originalColor;
        PlayerUnitEnterAnimation();
        EnemyUnityEnterAnimation();
    }

    public void PlayerUnitEnterAnimation()
    {
        PlayerUnit playerUnit = FindObjectOfType<PlayerUnit>();

        if (playerUnit != null)
        {
            Vector3 originalPos = playerUnit.transform.localPosition;
            playerUnit.transform.localPosition = new Vector3(-500f, playerUnit.transform.localPosition.y);
            playerUnit.transform.DOLocalMoveX(originalPos.x, 1f);
        }
    }

    public void EnemyUnityEnterAnimation()
    {
        image.transform.localPosition = new Vector3(500f, originalPos.y);
        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public void PlayPlayerAttackAnimation()
    {
        var sequence = DOTween.Sequence();

        PlayerUnit playerUnit = FindObjectOfType<PlayerUnit>();

        if (playerUnit != null)
        {
            Vector3 originalPos = playerUnit.transform.localPosition;
            sequence.Append(playerUnit.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f));
            sequence.Append(playerUnit.transform.DOLocalMoveX(originalPos.x, 0.25f));
        }
    }

    public void EnemyPlayerAttackAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveX(originalPos.x - 50f, 0.25f));
        sequence.Append(image.transform.DOLocalMoveX(originalPos.x, 0.25f));
        
    }

    public void EnemyPlayHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.red, 0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
    }

    public void PlayerPlayHitAnimation()
    {
        var sequence = DOTween.Sequence();

        PlayerUnit playerUnit = FindObjectOfType<PlayerUnit>();

        if (playerUnit != null)
        {
            Image playerImage = playerUnit.GetComponent<Image>();
            if (playerImage != null)
            {
                sequence.Append(playerImage.DOColor(Color.red, 0.1f));
                sequence.Append(playerImage.DOColor(originalColor, 0.1f));
            }
        }
    }

    public void PlayEnemyFaintedAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 150f, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));
    }

    public void PlayPlayerFaintedAnimation()
    {
        var sequence = DOTween.Sequence();

        PlayerUnit playerUnit = FindObjectOfType<PlayerUnit>();

        if (playerUnit != null)
        {
            Image playerImage = playerUnit.GetComponent<Image>();
            Vector3 originalPos = playerUnit.transform.localPosition;
            sequence.Append(playerImage.transform.DOLocalMoveY(originalPos.y - 150f, 0.5f));
            sequence.Join(playerImage.DOFade(0f, 0.5f));
        }
    }

}
