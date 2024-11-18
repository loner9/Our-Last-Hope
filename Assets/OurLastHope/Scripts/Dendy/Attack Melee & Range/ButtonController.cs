using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite selectedSprite;

    public GameObject panelToShow;
    public GameObject panelToClose;
    public string sceneToLoad;

    private Button button;
    private Image buttonImage;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // Set sprite default ke idle
        button.image.sprite = idleSprite;

        // Assign event listener untuk button click
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        // Reset semua button ke idle sprite
        ButtonController[] allButtons = FindObjectsOfType<ButtonController>();
        foreach (ButtonController btn in allButtons)
        {
            btn.ResetToIdle();
        }

        // Set selected sprite ke button yang dipencet
        buttonImage.sprite = selectedSprite;

        // Fungsi tambahan
        if (panelToShow != null)
        {
            panelToShow.SetActive(true);
        }

        if (panelToClose != null)
        {
            panelToClose.SetActive(false);
        }

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        // Untuk keluar dari game
        // Application.Quit();
    }

    public void ResetToIdle()
    {
        buttonImage.sprite = idleSprite;
    }
}
