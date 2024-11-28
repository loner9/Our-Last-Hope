using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    public Button loadGameButton;

    void OnEnable()
    {
        if (!PlayerPrefs.HasKey("LastLevel"))
        {
            loadGameButton.interactable = false;
        }
        else
        {
            loadGameButton.interactable = true;
        }
    }

    public void OnLoadGameButtonClick()
    {
        // Panggil fungsi LoadLastLevel yang ada pada script LoadGame
        FindObjectOfType<LoadGame>().LoadLastLevel();
    }
}
