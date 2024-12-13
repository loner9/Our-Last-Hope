using UnityEngine;

public class DialogueCompletionActivator : MonoBehaviour
{
    [SerializeField] private DialogueUIAwake dialogueUI;
    [SerializeField] private GameObject objectToActivate;

    private void Start()
    {
        if (dialogueUI == null)
        {
            Debug.LogError("DialogueUIAwake is not assigned.");
        }

        if (objectToActivate == null)
        {
            Debug.LogError("Object to activate is not assigned.");
        }
        else
        {
            Debug.Log("Object to activate: " + objectToActivate.name);
        }

        // Subscribe to the dialogue close event
        dialogueUI.OnDialogueClose += HandleDialogueClose;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the dialogue close event
        dialogueUI.OnDialogueClose -= HandleDialogueClose;
    }

    private void HandleDialogueClose()
    {
        Debug.Log("Dialog selesai, mengaktifkan object.");
        ActivateObject();
    }

    private void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            this.gameObject.SetActive(false);
            Debug.Log("Object activated: " + objectToActivate.name);
        }
        else
        {
            Debug.LogError("Object to activate is not assigned.");
        }
    }
}
