using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void LoadLastLevel()
    {
        if (PlayerPrefs.HasKey("LastLevel"))
        {
            int lastLevel = PlayerPrefs.GetInt("LastLevel");

            // Periksa apakah level yang disimpan berada dalam rentang level yang valid
            if (lastLevel > 0 && lastLevel < SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log("Loading level: " + lastLevel);
                SceneManager.LoadScene(lastLevel);
            }
            else
            {
                Debug.Log("Invalid saved level, loading default level 1.");
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            Debug.Log("No save data found");
            //SceneManager.LoadScene(1);
        }
    }
}
