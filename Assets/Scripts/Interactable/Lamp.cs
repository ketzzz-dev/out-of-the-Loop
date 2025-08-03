using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Lamp : Interactable
{
    [SerializeField] private Item lightBulb;
    [SerializeField] private TextAsset dialogueHasBulb;
    [SerializeField] private TextAsset dialogueNoBulb;
    [SerializeField] private bool hasBulb, bulbTaken;

    private bool leaveLamp, isInteracting;
    private DialogueGraph hasBulbDialogue;
    private DialogueGraph noBulbDialogue;
	private void Start()
    {
        hasBulbDialogue = JsonUtility.FromJson<DialogueGraph>(dialogueHasBulb.text);
        noBulbDialogue = JsonUtility.FromJson<DialogueGraph>(dialogueNoBulb.text);

        DialogueManager.instance.onDialogueChanged += SetToBulb;
        DialogueManager.instance.onDialogueEnded += Bulb;

        leaveLamp = false;
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
        //Don't delete this or else all lamps will follow all booleans
        if (!isInteracting)
        {
            return;
        }

        print(node);
        if (node == "takeBulb" && hasBulb && !itemCheck())
        {
            print("currently taking a bulb...");
            bulbTaken = true;
            hasBulb = false;
        } 
        if (node == "placeBulb" && !hasBulb && !itemCheck())
        {
            bulbTaken = false;
            hasBulb = true;
        }
        if (node == "leaveLamp")
        {
            leaveLamp = true; 
        }
    }

    private void Bulb()
    {
        //Don't delete this or else all lamps will follow all booleans
        if (!isInteracting)
        {
            return;
        }

        isInteracting = false;

        if (bulbTaken && !hasBulb && !leaveLamp)
        {
            //bulbTaken = false;
            Inventory.instance.SetHeldItem(lightBulb);

		    DialogueManager.instance.SetCondition("isDarkRoom", true);
		    DialogueManager.instance.SetCondition("haveBulbItem", true);
            //DialogueManager.instance.SetCondition("noBulbItem", true);

		    print("Bulb was taken");
        } 
		else if (!bulbTaken && hasBulb && !leaveLamp && !itemCheck())
        {
            Inventory.instance.ClearHeldItem();

            DialogueManager.instance.SetCondition("isDarkRoom", false);
            DialogueManager.instance.SetCondition("haveBulbItem", false);
            //DialogueManager.instance.SetCondition("noBulbItem", false);

            print("Bulb was returned");
        } else 
        {
            return;
        }
    }

	public override void Interact() 
    {
        //Don't delete this or else all lamps will follow all booleans
        if (isInteracting)
        {
            return;    
        }

        isInteracting = true;

        //Determines dialogue path with hasBulb boolean
        if (hasBulb)
        {
		    DialogueManager.instance.StartDialogue(hasBulbDialogue);
            print("Starting hasBulbDialogue...");
        } else
        {
            DialogueManager.instance.StartDialogue(noBulbDialogue);
            print("Starting noBulbDialogue...");
        }

        //Tells you that your inventory is full and its not a lightbulb
        if (itemCheck())
        {
            print("You have the wrong item.");
            DialogueManager.instance.SetCondition("haveBulbItem", true);
        }
	}
    
    //Do you have an item other than a lightbulb or not?
    private bool itemCheck()
    {
        return Inventory.instance.heldItem != null && Inventory.instance.heldItem != lightBulb;
    }
}

/*Lamp FlowChart

Has a bulb, empty inventory:
Interact with lamp -> take bulb -> no more bulb + bulb in inventory
hasBulb, !bulbTaken -> !hasBulb, bulbTaken

Has a bulb, bulb in inventory:
Interact with lamp -> take bulb -> can't take bulb

No bulb, bulb in inventory:
Interact with lamp -> place bulb -> has a bulb + no more bulb in inventory

No bulb, empty inventory:
Interact with lamp -> place bulb -> can't place bulb
 
 */
