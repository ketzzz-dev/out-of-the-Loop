public class Bed : Interactable
{
    private bool shouldSleep;

    private void Start()
    {
        DialogueManager.instance.onDialogueChanged += SetToSleep;
        DialogueManager.instance.onDialogueEnded += Sleep;
    }

    private void SetToSleep(string content, string node)
    {
        if (node != "shouldSleep")
        {
            return;
        }

        shouldSleep = true;
    }

    private void Sleep()
    {
        if (!shouldSleep)
        {
            return;
        }

        shouldSleep = false;

        print("sleep");

        WakeUpManager.instance.Sleep();
    }
}
