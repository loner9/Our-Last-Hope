using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueLevelResponseEvent
{
    [HideInInspector] public string name;
    [SerializeField] private UnityEvent onPickedResponse;

    public UnityEvent OnPickedResponse => onPickedResponse;
}
