using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    void OnLevelWasLoaded(int level)
    {
        // Periksa apakah level ini adalah main menu
        string sceneName = SceneManager.GetSceneByBuildIndex(level).name;
        if (sceneName != "UI_Main Menu") // Gantilah "UI_Main Menu" dengan nama scene main menu Anda
        {
            // Simpan level terakhir yang dicapai pemain
            PlayerPrefs.SetInt("LastLevel", level);
        }
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void Update()
    {
        // Tombol untuk kembali ke main menu
        if (Input.GetKeyDown(KeyCode.L))
        {
            GoToMainMenu();
        }
    }

    void GoToMainMenu()
    {
        string mainMenuSceneName = "UI_Main Menu"; // Gantilah dengan nama scene main menu Anda
        SceneManager.LoadScene(mainMenuSceneName);
        Debug.Log("Returning to main menu.");
    }
}
