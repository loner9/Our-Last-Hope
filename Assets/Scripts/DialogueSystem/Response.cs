using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private string sceneName;

    public string ResponseText => responseText;
    public DialogueObject DialogueObject => dialogueObject;
    public string SceneName => sceneName;
}
