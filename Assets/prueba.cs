using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class prueba : MonoBehaviour
{

    [SerializeField] private MoneyManager moneyManager;


    private void Start()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box.gameObject.CompareTag("Player"))
        {
            moneyManager.AddMoney(100);
            Debug.Log("Colision");
            Destroy(gameObject);
        }
    }
}
