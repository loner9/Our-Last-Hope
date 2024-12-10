using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueLevelUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;

    public bool IsOpen { get; private set; }

    private ResponseLevelHandler responseHandler;
    private DialogueLevelTypewriterEffect typewriterEffect;

    public PlayerMovement PMD;
    public WeaponManager WM;

    private string nextSceneName;

    private void Start()
    {
        typewriterEffect = GetComponent<DialogueLevelTypewriterEffect>();
        responseHandler = GetComponent<ResponseLevelHandler>();

        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
        PMD.enabled = false;
        WM.enabled = false;
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();

            // Jika ada nama scene yang disetel, muat scene tersebut setelah dialog selesai
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                LoadScene(nextSceneName);
            }
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                typewriterEffect.Stop();
            }
        }
    }

    public void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        PMD.enabled = true;
        WM.enabled = true;
    }

    public void SetNextScene(string sceneName)
    {
        nextSceneName = sceneName;
    }

    public void LoadScene(string sceneName)
    {
        // Fungsi untuk memuat scene baru berdasarkan nama scene yang diberikan
        SceneManager.LoadScene(sceneName);
        Debug.Log("Loading scene: " + sceneName);
    }
}
