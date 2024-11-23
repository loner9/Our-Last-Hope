using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Player player;
    public Player Player => player;
    private PlayerControls inputs;
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private GameObject invtory;

    private void Awake()
    {
        Instance = this;
        inputs = new PlayerControls();

        inputs.UI.Pause.performed += ctx => TogglePause();
    }

    public void TogglePause()
    {
        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            PauseManager.Instance.Resume();
            invtory.GetComponent<InventoryUI>().enabled = true;
        }else
        {
            pausePanel.SetActive(true);
            PauseManager.Instance.Pause();
            invtory.GetComponent<InventoryUI>().enabled = false;
        }
    }

    public void ToMainMenu(){
        SceneManager.LoadScene("UI_Main Menu");
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}