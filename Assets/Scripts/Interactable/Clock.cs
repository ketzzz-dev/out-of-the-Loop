using UnityEngine;

public class Clock : Interactable
{
    [SerializeField] private TextAsset dialogueFile;

    private DialogueGraph dialogueGraph;
    private bool shouldWakeUp;

    private void Start()
    {
        dialogueGraph = JsonUtility.FromJson<DialogueGraph>(dialogueFile.text);

        DialogueManager.instance.onDialogueChanged += SetToWakeUp;
        DialogueManager.instance.onDialogueEnded += WakeUp;
    }

    private void OnDestroy()
    {
        if (DialogueManager.instance == null)
        {
            return;
        }
        
        DialogueManager.instance.onDialogueChanged -= SetToWakeUp;
        DialogueManager.instance.onDialogueEnded -= WakeUp;
    }

    public override void Interact()
    {
        DialogueManager.instance.StartDialogue(dialogueGraph);
    }

    private void SetToWakeUp(string content, string node)
    {
        if (node != "shouldWake")
        {
            return;
        }

        shouldWakeUp = true;
    }

    private void WakeUp()
    {
        if (!shouldWakeUp)
        {
            return;
        }

        shouldWakeUp = false;

        print("wake");  

        WakeUpManager.instance.WakeUp();
    }
}
