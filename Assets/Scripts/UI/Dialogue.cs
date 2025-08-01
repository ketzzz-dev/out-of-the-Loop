using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI subtitles;
    [SerializeField] private float typingRate;

    [SerializeField] private GameObject options;
    [SerializeField] private GameObject optionPrefab;

    private bool isTyping;

    private void Start()
    {
        subtitles.enabled = false;

        DialogueManager.instance.onDialogueStarted += OnDialogueStarted;
        DialogueManager.instance.onDialogueChanged += OnDialogueChanged;
        DialogueManager.instance.onDialogueEnded += OnDialogueEnded;

        DialogueManager.instance.onSelectionStarted += OnSelectionStarted;
        DialogueManager.instance.onSelectionEnded += OnSelectionEnded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isTyping)
            {
                subtitles.maxVisibleCharacters = subtitles.text.Length;

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
        foreach (var id in labels)
        {
            var option = Instantiate(optionPrefab, options.transform);
            var button = option.GetComponent<Button>();
            var label = option.GetComponentInChildren<TextMeshProUGUI>();

            button.onClick.AddListener(() =>
            {
                DialogueManager.instance.SelectBranch(id);
            });

            label.text = id;
        }
    }
    private void OnSelectionEnded()
    {
        foreach (Transform child in options.transform) {
            Destroy(child.gameObject);
        }
    }

    private void ShowDialogue(string content)
    {
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