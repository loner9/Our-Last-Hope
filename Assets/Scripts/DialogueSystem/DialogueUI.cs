using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;

    public bool IsOpen { get; private set; }

    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;

    public PlayerMovement PlayerMovement;
    public WeaponManager PlayerWeaponManager;
    public Animator PlayerAnimator;
    public PlayerAim PlayerAim;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();

        if (typewriterEffect == null) Debug.LogError("TypewriterEffect is not assigned.");
        if (responseHandler == null) Debug.LogError("ResponseHandler is not assigned.");
        if (textLabel == null) Debug.LogError("TextLabel is not assigned.");
        if (dialogueBox == null) Debug.LogError("DialogueBox is not assigned.");

        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        if (dialogueObject == null)
        {
            Debug.LogError("DialogueObject is null.");
            return;
        }

        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
        PlayerMovement.enabled = false;
        PlayerWeaponManager.enabled = false;
        PlayerAnimator.enabled = false;
        PlayerAim.enabled = false;
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
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
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
        PlayerMovement.enabled = true;
        PlayerWeaponManager.enabled = true;
        PlayerAnimator.enabled = true;
        PlayerAim.enabled = true;
    }
}
