using UnityEngine;

public class SL2Door : Interactable
{
    [SerializeField] private int index;
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private Transform player;

    private void OpenDoor()
    {
        Debug.Log("Door unlocked!");

        if (index >= 0)
        {
            WakeUpManager.instance.MovePlayer(index);
        }
        else
        {
            if(player !=null)
            {
                player.position = spawnpoint.position;
            }
        }
    }

    public override void Interact()
    {
        OpenDoor();
    }
}