using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class GachaManager : MonoBehaviour
{
    [SerializeField] private GachaRate[] gacha;
    [SerializeField] private Transform parent, pos;
    [SerializeField] private GameObject characterCardGO;

    GameObject characterCard;

    Cards card;
    private void CreateCharacterCard()
    {
        characterCard = Instantiate(characterCardGO, pos.position, Quaternion.identity) as GameObject;
        characterCard.transform.SetParent(parent);
        characterCard.transform.localScale = Vector3.one;
        card = characterCard.GetComponent<Cards>();
    }
    public void Gacha()
    {
        if(characterCard == null) // Cambiarlo en un futuro por dinero o monedas del juego
        {
            CreateCharacterCard();

            int rnd = UnityEngine.Random.Range(1, 101);
            for(int i = 0; i < gacha.Length; i++)
            {
                if (rnd <= gacha[i].rate)
                {
                    card.card = Reward(gacha[i].rarity);
                    characterCard.SetActive(true);
                    return;
                }
            }
        } else
        {
            Destroy(characterCard);
            CreateCharacterCard();

            int rnd = UnityEngine.Random.Range(1, 101);
            for (int i = 0; i < gacha.Length; i++)
            {
                if (rnd <= gacha[i].rate)
                {
                    card.card = Reward(gacha[i].rarity);
                    characterCard.SetActive(true);
                    return;
                }
            }
        }
    }

    public int Rates(R rarity)
    {
        GachaRate gr = Array.Find(gacha, rt => rt._rarity == rarity);
        if(gr != null)
        {
            return gr.rate;
        } else
        {
            return 0;
        }
    }


    CardInfo Reward(string rarity)
    {
        GachaRate gr = Array.Find(gacha, rt => rt.rarity == rarity);
        CardInfo[] reward = gr.reward;

        int rnd = UnityEngine.Random.Range(0, reward.Length);
        return reward[rnd];
    }
}

[CustomEditor(typeof(GachaManager))]

public class GachaEditor : Editor
{

    public int Common, Uncommon, Rare, Epic, Legend;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        GachaManager gm = (GachaManager)target;

        Common = EditorGUILayout.IntField("Common", (gm.Rates(R.Common) - gm.Rates(R.Uncommon)));
        Uncommon = EditorGUILayout.IntField("Uncommon", (gm.Rates(R.Uncommon) - gm.Rates(R.Rare)));
        Rare = EditorGUILayout.IntField("Rare", (gm.Rates(R.Rare) - gm.Rates(R.Epic)));
        Epic = EditorGUILayout.IntField("Epic", (gm.Rates(R.Epic) - gm.Rates(R.Legend)));
        Legend = EditorGUILayout.IntField("Legend", gm.Rates(R.Legend));
    }
}


