using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : MonoBehaviour
{
    [SerializeField] GameObject mana;

    public void SetMana(float mana)
    {
        this.mana.transform.localScale = new Vector3(mana, 1f);
    }
}
