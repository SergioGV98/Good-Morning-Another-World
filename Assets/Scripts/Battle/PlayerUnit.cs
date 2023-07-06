using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : MonoBehaviour
{
    [SerializeField] PlayerBase _base;
    [SerializeField] byte level;
    [SerializeField] bool isPlayerUnit;

    public Player player { get; set; }

    private Image image;
    private Sprite originalSprite;
    private Vector3 originalPosition;
    private Color originalColor;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Setup()
    {
        player = new Player(_base, level);
        if (isPlayerUnit)
        {
            image.sprite = player.Base.FrontSprite;
            originalSprite = image.sprite;
            originalPosition = transform.localPosition;
            originalColor = image.color;
        }
    }

    public void ResetSpriteAndPosition()
    {
        if (isPlayerUnit)
        {
            image.sprite = originalSprite;
            transform.localPosition = originalPosition;
            image.color = originalColor;
        }
    }
}