using UnityEngine;

public class Lamp : Interactable
{
    private bool bulbTaken;
    [SerializeField] private Item lightBulb;
    [SerializeField] private TextAsset dialogueFile;

    private DialogueGraph dialogueGraph;
	private void Start()
    {
        dialogueGraph = JsonUtility.FromJson<DialogueGraph>(dialogueFile.text);

        DialogueManager.instance.onDialogueChanged += SetToBulb;
        DialogueManager.instance.onDialogueEnded += Bulb;
    }
   
    private void OnDestroy()
    {
        if (DialogueManager.instance == null)
        {
            return;
        }

        DialogueManager.instance.onDialogueChanged -= SetToBulb;
        DialogueManager.instance.onDialogueEnded -= Bulb;
    }

    private void SetToBulb(string content, string node)
    {
        if (node != "takeBulb")
        {
            return;
        }

        bulbTaken = true;
    }

    private void Bulb()
    {
        if (!bulbTaken)
        {
            return;
        }

        bulbTaken = false;
        Inventory.instance.SetHeldItem(lightBulb);

		DialogueManager.instance.SetCondition("isDarkRoom", true);
		DialogueManager.instance.SetCondition("bulbTaken", true);

		print("Bulb was taken");
    }

	public override void Interact() {
		DialogueManager.instance.StartDialogue(dialogueGraph);
	}
}
