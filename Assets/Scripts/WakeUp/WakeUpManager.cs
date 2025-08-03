using UnityEngine;
using UnityEngine.SceneManagement;

public class WakeUpManager : MonoBehaviour
{
    public static WakeUpManager instance { get; private set; }

    [Header("Spawnpoint & Room Settings")]
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private int roomNumber = 0;
    [SerializeField] private int steps = 1;

    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName;
    [SerializeField] private Transform player;
    [SerializeField] private float timeLimit;

    private bool justMoved = false;
    private float timer;

    void Awake()
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

    void Update()
    {
        if (justMoved)
        {
            timer += Time.deltaTime;
            if (timer > timeLimit)
            {
                justMoved = false;
                timer = 0;
            }
        }
    }

    // Moves to the previous spawn point (Wake Up).
    public void WakeUp()
    {
        if (!justMoved)
        {
            if (roomNumber <= 0)
            {
                return;
            }

            roomNumber = Mathf.Max(roomNumber - steps, 0);
            MovePlayer();
        }
    }

    // Moves to the next spawn point (Sleep).
    public void Sleep()
    {
        if (!justMoved)
        {
            if (roomNumber >= spawnpoints.Length - 1)
            {
                // TryLoadNextScene();
                return;
            }

            roomNumber = Mathf.Min(roomNumber + steps, spawnpoints.Length - 1);
            MovePlayer();
        }
    }
    
    // Teleports player to current room's spawn point.
    public void MovePlayer()
    {
        if (player != null && spawnpoints.Length > 0)
        {
            player.position = spawnpoints[roomNumber].position;
            Debug.Log($"Player moved to room {roomNumber} at {player.position}");

            justMoved = true;
        }
    }

    // Teleports player to current room's spawn point.
    public void MovePlayer(int index)
    {
        if (player != null && spawnpoints.Length > 0 && !justMoved)
        {
            player.position = spawnpoints[index].position;
            Debug.Log($"Player moved to room {index} at {player.position}");
            roomNumber = index;

            justMoved = true;
        }
    }

    // Attempts to load the next scene if available.
    public void TryLoadNextScene()
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
