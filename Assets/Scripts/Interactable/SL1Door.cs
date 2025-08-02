using UnityEngine;

public class SL1Door : Interactable
{
    //change header and field for other key&locks i guess
    [Header("Door Settings")]
    [SerializeField] private string requiredKey = "door key"; //holy shit please make sure its the same name as the component dont be like me 
    [SerializeField] private TextAsset dialogueFile;

    private DialogueGraph dialogueGraph;

    private bool isOpen = false;

    private void Start()
    {
        dialogueGraph = JsonUtility.FromJson<DialogueGraph>(dialogueFile.text);
    }

    private void OpenDoor()
    {
        isOpen = true;
        Debug.Log("Door unlocked!");

        //rotates like a door
        transform.Rotate(0, -90, 0);

        //use up item
        Inventory.instance.ClearHeldItem();
    }

    public override void Interact()
    {
        if (isOpen) 
        {
            Debug.Log("Door is already unlocked.");
            return;
        }

        if (Inventory.instance.heldItem?.name == requiredKey) 
        {
            OpenDoor();
            return;
        }

        if (Inventory.instance.heldItem != null)
        {
            Debug.Log("This item can't unlock the door.");
        }
        else
        {
            Debug.Log("You're not holding any item.");
        }

        if (!isOpen)
        {
            DialogueManager.instance.StartDialogue(dialogueGraph);
        }
    }
}