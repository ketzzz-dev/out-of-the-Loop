using UnityEngine;

public class ClockPopupTrigger : MonoBehaviour
{
    public GameObject popupPanel;
    public KeyCode interactKey = KeyCode.F;

    private bool isPlayerNearby = false;
    private bool isPopupVisible = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(interactKey))
        {
            isPopupVisible = !isPopupVisible;
            popupPanel.SetActive(isPopupVisible);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            popupPanel.SetActive(false);
            isPopupVisible = false;
        }
    }
}
