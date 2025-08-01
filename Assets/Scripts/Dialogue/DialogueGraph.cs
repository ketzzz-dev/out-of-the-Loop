using System;

[Serializable]
public struct DialogueGraph
{
    [Serializable]
    public struct DialogueNode
    {
        public string id;
        public string content;

        public DialogueConnection[] connections;
    }

    [Serializable]
    public struct DialogueConnection
    {
        public string label;

        public string nextNode;
    }

    public DialogueNode[] nodes;
}