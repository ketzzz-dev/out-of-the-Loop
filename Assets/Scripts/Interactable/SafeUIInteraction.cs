using UnityEngine;
using TMPro;
using System.Collections;

public class SafeUIInteraction : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject safeUIPanel;
    public GameObject closedSafeImage;
    public GameObject openSafeImage;
    public GameObject keypadUI;
    public TMP_Text codeDisplay;
    public GameObject codeDisplayBackground;

    [Header("Safe Code Settings")]
    public string correctCode = "1234";
    private string currentInput = "";

    [Header("Interaction Settings")]
    public KeyCode interactKey = KeyCode.F;
    private bool isPlayerNearby = false;
    private bool isUIOpen = false;
    private bool safeOpened = false;

    [Header("Reward Item")]
    public GameObject rewardItem;

    void Start()
    {
        HideAllUI();
        if (rewardItem != null)
            rewardItem.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(interactKey) && !safeOpened)
            ToggleSafeUI();
    }

    public void PressNumber(string number)
    {
        if (currentInput.Length < 4)
        {
            currentInput += number;
            UpdateDisplay();
        }
    }

    public void Backspace()
    {
        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            UpdateDisplay();
        }
    }

    public void SubmitCode()
    {
        if (currentInput == correctCode)
            StartCoroutine(OpenSafeSequence());
        else
        {
            Debug.Log("Wrong code.");
            currentInput = "";
            UpdateDisplay();
        }
    }

    private IEnumerator OpenSafeSequence()
    {
        safeOpened = true;

        keypadUI.SetActive(false);
        SetCodeDisplayVisibility(false);

        closedSafeImage.SetActive(false);
        openSafeImage.SetActive(true);

        yield return new WaitForSeconds(2f);

        // Pakita ang Item
        if (rewardItem != null)
            rewardItem.SetActive(true);

        HideAllUI();
    }

    private void ToggleSafeUI()
    {
        isUIOpen = !isUIOpen;

        if (isUIOpen)
        {
            safeUIPanel.SetActive(true);
            closedSafeImage.SetActive(true);
            openSafeImage.SetActive(false);
            keypadUI.SetActive(true);
            SetCodeDisplayVisibility(true);

            currentInput = "";
            UpdateDisplay();
        }
        else
        {
            HideAllUI();
        }
    }

    private void HideAllUI()
    {
        isUIOpen = false;
        safeUIPanel.SetActive(false);
        closedSafeImage.SetActive(false);
        openSafeImage.SetActive(false);
        keypadUI.SetActive(false);
        SetCodeDisplayVisibility(false);
        currentInput = "";
        UpdateDisplay();
    }

    private void SetCodeDisplayVisibility(bool visible)
    {
        if (codeDisplay != null)
            codeDisplay.gameObject.SetActive(visible);
        if (codeDisplayBackground != null)
            codeDisplayBackground.SetActive(visible);
    }

    private void UpdateDisplay()
    {
        if (codeDisplay != null)
            codeDisplay.text = currentInput;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            HideAllUI();
        }
    }
}
