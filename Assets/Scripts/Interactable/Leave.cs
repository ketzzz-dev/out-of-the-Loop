using UnityEngine;

public class Leave : Interactable
{
    [SerializeField] private TextAsset dialogueFile;

    private DialogueGraph dialogueGraph;
    private bool shouldLeave;

    private void Start()
    {
        dialogueGraph = JsonUtility.FromJson<DialogueGraph>(dialogueFile.text);

        DialogueManager.instance.onDialogueChanged += SetToLeave;
        DialogueManager.instance.onDialogueEnded += HeadOut;
    }

    private void OnDestroy()
    {
        if (DialogueManager.instance == null)
        {
            return;
        }
        
        DialogueManager.instance.onDialogueChanged -= SetToLeave;
        DialogueManager.instance.onDialogueEnded -= HeadOut;
    }

    public override void Interact()
    {
        DialogueManager.instance.StartDialogue(dialogueGraph);
    }

    private void SetToLeave(string content, string node)
    {
        if (node != "shouldLeave")
        {
            return;
        }

        shouldLeave = true;
    }

    private void HeadOut()
    {
        if (!shouldLeave)
        {
            return;
        }

        shouldLeave = false;

        print("Leave");

        WakeUpManager.instance.TryLoadNextScene();
    }
}
