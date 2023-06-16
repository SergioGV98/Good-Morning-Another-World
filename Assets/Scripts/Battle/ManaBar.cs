using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : MonoBehaviour
{
    [SerializeField] GameObject mana;

    public void SetMana(float manaa)
    {
        mana.transform.localScale = new Vector3(manaa, 1f);
    }
}
