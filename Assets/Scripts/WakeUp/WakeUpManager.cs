using UnityEngine;
using UnityEngine.SceneManagement;

public class WakeUpManager : MonoBehaviour
{
    [Header("Spawnpoint & Room Settings")]
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private int roomNumber = 0;

    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName;

    private Transform player;

    void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;

        if (player == null)
        {
            Debug.LogError("PlayerMovement not found in scene!");
            return;
        }
    }

    void Update()
    {
        // Temporary controls for debugging
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            WakeUp();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Sleep();
        }
    }

    // Moves to the previous spawn point (Wake Up).
    public void WakeUp()
    {
        if (roomNumber <= 0)
        {
            Debug.Log("Can't move backward - already at first room.");
            return;
        }

        roomNumber--;
        MovePlayer();
    }

    // Moves to the next spawn point (Sleep).
    public void Sleep()
    {
        if (roomNumber >= spawnpoints.Length - 1)
        {
            TryLoadNextScene();
            return;
        }

        roomNumber++;
        MovePlayer();
    }

    // Moves backward by a specific number of rooms.
    public void WakeUp(int steps)
    {
        roomNumber = Mathf.Max(roomNumber - steps, 0);
        MovePlayer();
    }

    
    // Moves forward by a specific number of rooms.
    public void Sleep(int steps)
    {
        int targetRoom = roomNumber + steps;

        if (targetRoom >= spawnpoints.Length)
        {
            roomNumber = spawnpoints.Length - 1;
            TryLoadNextScene();
            return;
        }

        roomNumber = targetRoom;
        MovePlayer();
    }

    
    // Teleports player to current room's spawn point.
    private void MovePlayer()
    {
        if (player != null && spawnpoints.Length > 0)
        {
            player.position = spawnpoints[roomNumber].position;
        }
    }

    
    // Attempts to load the next scene if available.
    private void TryLoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("Can't move forward - last room reached and no next scene assigned.");
        }
    }
}
