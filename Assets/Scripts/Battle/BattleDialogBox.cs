using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
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

}
