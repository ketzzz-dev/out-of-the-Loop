using UnityEngine;

public class SL1Door : Interactable
{
    //change header and field for other key&locks i guess
    [Header("Door Settings")]
    [SerializeField] private Item requiredKey;
    [SerializeField] private string requiredKeyName;
    [SerializeField] private TextAsset doorOpen, doorClosed;

    private DialogueGraph dialogueGraphOpen, dialogueGraphClosed;

    private bool isOpen = false;

    private void Start()
    {
        dialogueGraphOpen = JsonUtility.FromJson<DialogueGraph>(doorOpen.text);
        dialogueGraphClosed = JsonUtility.FromJson<DialogueGraph>(doorClosed.text);
    }

    void OnMouseDown()  
    {
        if (isOpen)
        {
            Debug.Log("Door is already unlocked.");
            return;
        }

        if (Inventory.instance.heldItem == null)
        {
            Debug.Log("You're not holding any item.");
            return;
        }

        if ((Inventory.instance.heldItem == requiredKey || Inventory.instance.heldItem.name == requiredKeyName)&& !isOpen)
        {
            OpenDoor();
        }
        else
        {
            Debug.Log("This item can't unlock the door.");
        }
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
        if (!isOpen)
        {
            DialogueManager.instance.StartDialogue(dialogueGraphClosed);
        }
        else
        {
            DialogueManager.instance.StartDialogue(dialogueGraphOpen);
            WakeUpManager.instance.TryLoadNextScene();
            Debug.Log("No dialogue triggered because door is open.");
        }
    }
}