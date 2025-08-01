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
    }

    void Update()
    {
        //remove both if statements when not needed anymore
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            WakeUp();
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Sleep();
        }
    }

    //Wake Up function, moves player to the previous spawn point
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

    //Sleep function, moves player to the next spawn point
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

    //Loads scene of roomNumber and checks if the user is on the last room
    void NextScene()
    {
        if (roomNumber >= spawnpoints.Length - 1)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    //Loads scene regardless of roomNumber
    void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    //Only if you need to load a different level/area
    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void MovePlayer()
    {
        player.position = spawnpoints[roomNumber].position;
    }
}
