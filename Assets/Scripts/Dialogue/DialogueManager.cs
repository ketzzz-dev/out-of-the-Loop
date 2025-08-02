using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }

    // event delagates
    public event Action<string> onDialogueStarted;
    public event Action<string, string> onDialogueChanged;
    public event Action onDialogueEnded;

    public event Action<string[]> onSelectionStarted;
    public event Action onSelectionEnded;

	// Dialogue Choices
	private Dictionary<string, bool> dialogueConditions = new Dictionary<string, bool>();

    // internal data
    private DialogueGraph currentGraph;
    private DialogueGraph.DialogueNode currentNode;

    private Dictionary<string, DialogueGraph.DialogueNode> currentNodes;

    // flow state
    public bool isActive { get; private set; }
    public bool isSelecting { get; private set; }

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void StartDialogue(DialogueGraph graph)
    {
        if (isActive)
        {
            Debug.LogWarning("Dialogue is already active");

            return;
        }
        if (graph.nodes == null || graph.nodes.Length == 0)
        {
            Debug.LogError("Dialogue graph is empty or invalid!");

            return;
        }

        currentNodes = graph.nodes.ToDictionary(node => node.id);

        if (!currentNodes.TryGetValue("start", out currentNode))
        {
            Debug.LogError("Dialogue graph has no start node");

            return;
        }

        currentGraph = graph;
        isActive = true;

        onDialogueStarted?.Invoke(currentNode.content);
    }
    public void EndDialogue()
    {
        if (!isActive)
        {
            Debug.LogWarning("Dialogue is already inactive");

            return;
        }

        isActive = false;

        onDialogueEnded?.Invoke();
    }

    public void AdvanceDialogue()
    {
        if (!isActive)
        {
            Debug.LogWarning("Dialogue is inactive");

            return;
        }

        var connections = currentNode.connections;

        if (connections == null || connections.Length == 0) // No connections (end of dialogue)
        {
            EndDialogue();

            return;
        }
        else if (connections.Length == 1) // One connection (linear)
        {
            var nextNode = connections[0].nextNode;

            if (!currentNodes.TryGetValue(nextNode, out currentNode))
            {
                Debug.LogError($"Dialogue graph has no {nextNode} node");
                EndDialogue();

                return;
            }

            onDialogueChanged?.Invoke(currentNode.content, currentNode.id);
        }
        else // Variable connections (branching)
        {
            HandleBranching(connections);
        }
    }

    public void SelectBranch(string label)
    {
        if (!isActive || !isSelecting)
        {
            Debug.LogWarning("Dialogue is not in selection mode");

            return;
        }

        string nextNode = null;

        foreach (var connection in currentNode.connections)
        {
            if (connection.label == label)
            {
				nextNode = connection.nextNode;
				if (connection.condition != null) {
					if (dialogueConditions.ContainsKey(connection.condition))
						nextNode = dialogueConditions[connection.condition] ? connection.nextNodeTrue : connection.nextNode;
				}
            }
        }
        
        if (!currentNodes.TryGetValue(nextNode, out currentNode))
        {
            Debug.LogError($"Dialogue graph has no {nextNode} node");
            EndDialogue();

            return;
        }

        isSelecting = false;
        onSelectionEnded?.Invoke();
        onDialogueChanged?.Invoke(currentNode.content, currentNode.id);
    }

    private void HandleBranching(DialogueGraph.DialogueConnection[] connections)
    {
        if (!isActive && isSelecting)
        {
            Debug.LogWarning("Dialogue is already selecting");

            return;
        }

        isSelecting = true;

        var labels = connections.Select(connection => connection.label).ToArray();

        onSelectionStarted?.Invoke(labels);
    }
	
	public void SetCondition(string condition, bool val) {
		dialogueConditions[condition] = val;
	}
}