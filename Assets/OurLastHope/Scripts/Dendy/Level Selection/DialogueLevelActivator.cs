using UnityEngine;

public class DialogueLevelActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    public AudioSource sc;

    public DialogueObject DialogueObject => dialogueObject; // Menambahkan properti ini

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            player.Interactable = this;
            if (sc != null)
            {
                sc.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            if (player.Interactable is DialogueLevelActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }
    }

    public void Interact(PlayerMovement player)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                player.DialogueUIExtended.AddResponseEvents(responseEvents.Events);
                break;
            }
        }

        player.DialogueUIExtended.ShowDialogue(dialogueObject);
    }
}
