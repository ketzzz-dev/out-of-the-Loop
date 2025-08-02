using UnityEngine;

public class Lamp : Interactable
{
    private bool bulbTaken;
    [SerializeField] private Item lightBulb;


    private void Start()
    {
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

		print("Bulb was taken");
    }
}
