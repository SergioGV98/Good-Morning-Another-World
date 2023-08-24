using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private int currentMoney;

    private void Start()
    {
        currentMoney = LoadMoney(); // Cargar el valor del dinero guardado
        UpdateMoneyDisplay(currentMoney); // Actualizar el texto en pantalla
    }

    public int GetMoneyAmount()
    {
        return currentMoney;
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        SaveMoney(currentMoney); // Guardar el valor del dinero actualizado
        UpdateMoneyDisplay(currentMoney); // Actualizar el texto en pantalla
        Debug.Log(currentMoney);
    }

    public void DeductMoney(int amount)
    {
        currentMoney -= amount;
        SaveMoney(currentMoney); // Guardar el valor del dinero actualizado
        UpdateMoneyDisplay(currentMoney); // Actualizar el texto en pantalla
    }

    private int LoadMoney()
    {
        return PlayerPrefs.GetInt("Money", 0); // Cargar el valor del dinero guardado en PlayerPrefs
    }

    private void SaveMoney(int amount)
    {
        PlayerPrefs.SetInt("Money", amount); // Guardar el valor del dinero en PlayerPrefs
        PlayerPrefs.Save();
    }

    public void UpdateMoneyDisplay(int amount)
    {
        moneyText.text = amount.ToString(); // Actualizar el texto en pantalla con el valor del dinero actualizado
        Debug.Log(currentMoney); ;
    }

}