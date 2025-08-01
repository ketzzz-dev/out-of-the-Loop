using UnityEngine;

public class WakeUpManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private int roomNumber = 0;
    private Transform player;

    void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;

        if (spawnpoints.Length > 0)
        {
            MovePlayer();
        }
            
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            WakeUp();
        }


        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Sleep();
        }
    }

    void WakeUp()
    {
        if (roomNumber >= spawnpoints.Length - 1)
        {
            Debug.Log("Can't wake up - already at last room");
            return;
        }

        roomNumber++;
        MovePlayer();
    }

    void Sleep()
    {
        if (roomNumber <= 0)
        {
            Debug.Log("Can't sleep - already at first room");
            return;
        }

        roomNumber--;
        MovePlayer();
    }

    void MovePlayer()
    {
        player.position = spawnpoints[roomNumber].position;
    }
}
