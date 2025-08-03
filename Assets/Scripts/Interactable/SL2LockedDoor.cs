using UnityEngine;

public class SL2LockedDoor : Interactable
{
    //change header and field for other key&locks i guess
    [Header("Locked Door Settings")]
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