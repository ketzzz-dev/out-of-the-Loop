using UnityEngine;
using UnityEngine.SceneManagement;

public class WakeUpManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private int roomNumber = 0;
    [SerializeField] private string nextSceneName;
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
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            WakeUp();
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Sleep();
        }
    }

    void WakeUp()
    {
        if (roomNumber <= 0)
        {
            Debug.Log("Can't sleep - already at first room");
            return;
        }

        roomNumber--;
        MovePlayer();
    }

    void Sleep()
    {
        if (roomNumber >= spawnpoints.Length - 1)
        {
            if (nextSceneName != null)
            {
                NextScene();
                return;
            }

            Debug.Log("Can't wake up - already at last level");
            return;
        }

        roomNumber++;
        MovePlayer();
    }

    void NextScene()
    {
        if (roomNumber >= spawnpoints.Length - 1)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    void MovePlayer()
    {
        player.position = spawnpoints[roomNumber].position;
    }
}
