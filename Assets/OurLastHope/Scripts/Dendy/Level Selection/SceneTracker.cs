using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    [SerializeField] private string initialSceneName = "UI_Main Menu"; // Nama scene yang bisa diatur melalui Inspector
    [SerializeField] private string levelOneSceneName = "Level1"; // Nama scene level 1 yang bisa diatur melalui Inspector

    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt(currentScene, 1); // Menyimpan state scene yang sedang dimainkan
    }

    private void Update()
    {
        // Reset PlayerPrefs ketika Left Shift + R ditekan
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayerPrefs();
        }
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(initialSceneName, 1); // Mengatur level yang bisa diakses sesuai dengan nilai di Inspector
        PlayerPrefs.SetInt(levelOneSceneName, 1); // Mengatur level 1 sesuai dengan nilai di Inspector

        Debug.Log($"PlayerPrefs telah direset. Hanya {initialSceneName} dan {levelOneSceneName} yang bisa diakses.");
    }
}
