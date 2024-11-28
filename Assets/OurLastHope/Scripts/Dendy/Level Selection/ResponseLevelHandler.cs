using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class ResponseLevelHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private GameObject responseContainerPrefab;
    [SerializeField] private GameObject responseButtonTemplatePrefab;

    private DialogueLevelUI dialogueUI;
    private ResponseEvent[] responseEvents;

    private List<GameObject> tempResponseContainers = new List<GameObject>();

    private void Start()
    {
        dialogueUI = GetComponent<DialogueLevelUI>();
        if (dialogueUI == null)
        {
            Debug.LogError("DialogueLevelUI tidak ditemukan pada GameObject.");
        }
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }

    public void ShowResponses(Response[] responses)
    {
        foreach (Response response in responses)
        {
            // Instantiate responseContainer
            GameObject responseContainer = Instantiate(responseContainerPrefab, responseBox.transform);
            responseContainer.SetActive(true);

            // Instantiate responseButtonTemplate inside the responseContainer
            GameObject responseButton = Instantiate(responseButtonTemplatePrefab, responseContainer.transform);
            responseButton.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;

            // Cek apakah scene sudah pernah dimainkan
            if (!string.IsNullOrEmpty(response.SceneName) && PlayerPrefs.GetInt(response.SceneName, 0) == 0)
            {
                responseButton.GetComponent<Button>().interactable = false; // Nonaktifkan tombol jika scene belum pernah dimainkan
            }
            else
            {
                responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));
            }

            tempResponseContainers.Add(responseContainer);
        }

        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response)
    {
        responseBox.gameObject.SetActive(false);

        foreach (GameObject container in tempResponseContainers)
        {
            Destroy(container);
        }
        tempResponseContainers.Clear();

        if (responseEvents != null)
        {
            int responseIndex = System.Array.IndexOf(responseEvents, response);
            if (responseIndex >= 0 && responseIndex < responseEvents.Length)
            {
                responseEvents[responseIndex].OnPickedResponse?.Invoke();
            }
        }

        responseEvents = null;

        if (!string.IsNullOrEmpty(response.SceneName))
        {
            SceneManager.LoadScene(response.SceneName);
        }
        else if (response.DialogueObject)
        {
            dialogueUI.ShowDialogue(response.DialogueObject);
        }
        else
        {
            dialogueUI.CloseDialogueBox();
        }
    }
}
