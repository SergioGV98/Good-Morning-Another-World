using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private MoneyManager moneyManager;

    private void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>(); // Buscar el objeto MoneyManager en la escena
    }

    private void UpdateMoneyDisplay(int amount)
    {
        moneyText.text = amount.ToString();
    }

    private void Update()
    {
        int currentMoney = GetMoneyAmount();
        UpdateMoneyDisplay(currentMoney);
    }

    private int GetMoneyAmount()
    {
        if (moneyManager != null)
        {
            return moneyManager.GetMoneyAmount(); // Obtener la cantidad de dinero del MoneyManager
        }

        return 0; // Si no se encuentra el MoneyManager, devolver un valor predeterminado
    }

}
