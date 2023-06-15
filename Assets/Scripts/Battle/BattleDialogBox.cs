using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject magicSelector;
    [SerializeField] GameObject magicDetails;
    [SerializeField] List<TextMeshProUGUI> actionTexts;
    [SerializeField] List<TextMeshProUGUI> magicTexts;
    [SerializeField] TextMeshProUGUI manaText;
    [SerializeField] TextMeshProUGUI magicTypeText;

    public void SetDialog(string dialogText)
    {
        this.dialogText.text = dialogText;
    }

    public IEnumerator TypeDialog(string dialogType)
    {
        dialogText.text = "";
        foreach (var letter in dialogType.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }

    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }

    public void EnableMagicSelector(bool enabled)
    {
        magicSelector.SetActive(enabled);
        magicDetails.SetActive(enabled);
    }

    public void UpdateActionSelection(byte selectedAction)
    {
        for(byte i = 0; i  < actionTexts.Count; i++)
        {
            if (i == selectedAction)
            {
                actionTexts[i].color = highlightedColor;
            } else
            {
                actionTexts[i].color = Color.black;
            }
        }
    }

    public void UpdateMagicSelection (byte selectedMagic, Move magic)
    {
        for (byte i = 0; i < actionTexts.Count; i++)
        {
            if(i == selectedMagic)
            {
                magicTexts[i].color = highlightedColor;
            } else
            {
                magicTexts[i].color = Color.black;
            }
        }

        manaText.text = $"Mana {magic.Base.Mana}";
        magicTypeText.text = magic.Base.Type.ToString();
    }

    public void SetMagicMovesNames(List<Move> magicMoves)
    {
        for (byte i = 0;i < magicTexts.Count; i++)
        {
            if(i < magicMoves.Count)
            {
                magicTexts[i].text = magicMoves[i].Base.Name;
            } else
            {
                magicTexts[i].text = "-";
            }
        }
    }

}
