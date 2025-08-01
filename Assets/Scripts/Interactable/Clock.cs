public class Clock : Interactable
{
    private bool shouldWakeUp;

    private void Start()
    {
        DialogueManager.instance.onDialogueChanged += SetToWakeUp;
        DialogueManager.instance.onDialogueEnded += WakeUp;
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
