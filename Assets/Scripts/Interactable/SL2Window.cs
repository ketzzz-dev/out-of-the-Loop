using UnityEngine;

public class SL2Window : Interactable
{
    //change header and field for other key&locks i guess
    [Header("Window Settings")]
    [SerializeField] private TextAsset dialogueFile;

    private DialogueGraph dialogueGraph;

    private void Start()
    {
        dialogueGraph = JsonUtility.FromJson<DialogueGraph>(dialogueFile.text);
    }

    public override void Interact()
    {
        DialogueManager.instance.StartDialogue(dialogueGraph);
    }
}
