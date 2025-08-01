using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    [SerializeField] private TextAsset dialogueFile;

    private DialogueGraph graph;

    private void Start()
    {
        graph = JsonUtility.FromJson<DialogueGraph>(dialogueFile.text);

        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!DialogueManager.instance.isActive)
            {
                DialogueManager.instance.StartDialogue(graph);
            }
        }
    }
}
