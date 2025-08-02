using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private TextAsset dialogueFile;

    public virtual void Interact()
    {
        if (dialogueFile == null)
        {
            return;
        }

        DialogueGraph graph = JsonUtility.FromJson<DialogueGraph>(dialogueFile.text);

        DialogueManager.instance.StartDialogue(graph);
    }
}