using UnityEngine;

public class Bed : Interactable
{
    [SerializeField] private TextAsset dialogueFile;

    private DialogueGraph dialogueGraph;
    private bool shouldSleep;

    private void Start()
    {
        dialogueGraph = JsonUtility.FromJson<DialogueGraph>(dialogueFile.text);

        DialogueManager.instance.onDialogueChanged += SetToSleep;
        DialogueManager.instance.onDialogueEnded += Sleep;
    }

    private void OnDestroy()
    {
        if (DialogueManager.instance == null)
        {
            return;
        }
        
        DialogueManager.instance.onDialogueChanged -= SetToSleep;
        DialogueManager.instance.onDialogueEnded -= Sleep;
    }

    public override void Interact()
    {
        DialogueManager.instance.StartDialogue(dialogueGraph);
    }

    private void SetToSleep(string content, string node)
    {
        if (node != "shouldSleep")
        {
            return;
        }

        shouldSleep = true;
    }

    private void Sleep()
    {
        if (!shouldSleep)
        {
            return;
        }

        shouldSleep = false;

        print("sleep");

        WakeUpManager.instance.Sleep();
    }
}
