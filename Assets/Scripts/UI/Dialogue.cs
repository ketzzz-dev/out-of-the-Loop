using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI subtitles;
    [SerializeField] private float typingRate;

    [Header("Options")]
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private int maxOptions = 4;

    private bool isTyping;
    private List<GameObject> optionPool = new List<GameObject>();

    private void Start()
    {
        subtitles.enabled = false;

        DialogueManager.instance.onDialogueStarted += OnDialogueStarted;
        DialogueManager.instance.onDialogueChanged += OnDialogueChanged;
        DialogueManager.instance.onDialogueEnded += OnDialogueEnded;

        DialogueManager.instance.onSelectionStarted += OnSelectionStarted;
        DialogueManager.instance.onSelectionEnded += OnSelectionEnded;

        for(int i = 0; i < maxOptions; i++)
        {
            var option = Instantiate(optionPrefab, options.transform);

            option.SetActive(false);
            optionPool.Add(option);
        }
    }

    private void OnDestroy()
    {
        if (DialogueManager.instance == null)
        {
            return;
        }
        
        DialogueManager.instance.onDialogueStarted -= OnDialogueStarted;
        DialogueManager.instance.onDialogueChanged -= OnDialogueChanged;
        DialogueManager.instance.onDialogueEnded -= OnDialogueEnded;

        DialogueManager.instance.onSelectionStarted -= OnSelectionStarted;
        DialogueManager.instance.onSelectionEnded -= OnSelectionEnded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isTyping)
            {
                StopAllCoroutines();

                subtitles.maxVisibleCharacters = subtitles.text.Length;
                isTyping = false;

                return;
            }
            if (DialogueManager.instance.isActive)
            {
                DialogueManager.instance.AdvanceDialogue();
            }
        }
    }

    private void OnDialogueStarted(string content)
    {
        if (subtitles == null)
        {
            return;
        }

        subtitles.enabled = true;

        ShowDialogue(content);
    }

    private void OnDialogueChanged(string content)
    {
        ShowDialogue(content);
    }

    private void OnDialogueEnded()
    {
        subtitles.enabled = false;
    }

    private void OnSelectionStarted(string[] labels)
    {
        if (optionPrefab == null || options == null)
        {
            Debug.LogError("UI references not set");

            return;
        }

        for (int i = 0; i < labels.Length; i++)
        {
            if (i >= maxOptions)
            {
                break;
            };
            
            var option = optionPool[i];

            option.SetActive(true);
            
            var button = option.GetComponent<Button>();
            var labelText = option.GetComponentInChildren<TextMeshProUGUI>();

            if (labelText)
            {
                labelText.text = labels[i];
            }
            
            string currentLabel = labels[i];

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => 
            {
                DialogueManager.instance.SelectBranch(currentLabel);
            });
        }
    }
    private void OnSelectionEnded()
    {
        options.SetActive(false);

        foreach (var option in optionPool)
        {
            option.SetActive(false);
        }
    }

    private void ShowDialogue(string content)
    {
        if (subtitles == null)
        {
            return;
        }

        StopAllCoroutines();

        isTyping = false;
        subtitles.text = content;
        subtitles.maxVisibleCharacters = 0;

        StartCoroutine(Typewrite());
    }

    private IEnumerator Typewrite()
    {
        isTyping = true;

        while (subtitles.maxVisibleCharacters < subtitles.text.Length)
        {
            subtitles.maxVisibleCharacters++;

            yield return new WaitForSecondsRealtime(typingRate);
        }

        isTyping = false;
    }
}