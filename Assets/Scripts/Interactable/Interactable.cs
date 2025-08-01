using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private TextAsset dialogueFile;

    public void Interact()
    {
        if (dialogueFile == null)
        {
            return;
        }

        DialogueGraph graph = JsonUtility.FromJson<DialogueGraph>(dialogueFile.text);

        DialogueManager.instance.StartDialogue(graph);
    }
}