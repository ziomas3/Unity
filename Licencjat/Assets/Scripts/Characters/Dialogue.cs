using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text dialogueText;
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private GameObject menu;
    private string[] dialogue;
    private int dialogueIndex;

    public void StartDialogue(string[] dialogue)
    {
        this.dialogue = dialogue;
        dialoguePanel.SetActive(true);
        dialogueText.text = dialogue[dialogueIndex];
        menu.GetComponent<Menu>().MyszkaOff();

    }
    public void NextLine()
    {
        dialogueIndex = Mathf.Min(dialogueIndex + 1, dialogue.Length);
        if (dialogueIndex >= this.dialogue.Length)
        {
            ResetDialogue();
        }
        else
        {
            dialogueText.text = dialogue[dialogueIndex];
        }
    }
    public void PreviousLine()
    {
        if (dialogueIndex == 0)
        {
            return;
        }
        else
        {
            dialogueIndex = Mathf.Max(dialogueIndex - 1, 0);
            dialogueText.text = dialogue[dialogueIndex];
        }
    }
    public void ResetDialogue()
    {
        dialogue = null;
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        menu.GetComponent<Menu>().MyszkaOn();
        dialogueIndex = 0;
    }
}
