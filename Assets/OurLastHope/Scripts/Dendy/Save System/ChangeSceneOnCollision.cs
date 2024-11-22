using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnCollision : MonoBehaviour
{
    public string targetSceneName;

    void Start()
    {
        // Debugging untuk memeriksa apakah nama scene telah ditetapkan
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError("Target scene name is not set.");
        }
        else
        {
            Debug.Log("Target scene name is set to " + targetSceneName);
        }

        // Debugging untuk memeriksa apakah objek dengan tag "Player" ada
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            Debug.LogError("Player object with tag 'Player' not found in the scene.");
        }
        else
        {
            Debug.Log("Player object found.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with object: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with Player detected.");
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.Log("Collision with non-Player object.");
        }
    }

    void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision stay detected with object: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision stay with Player detected.");
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.Log("Collision stay with non-Player object.");
        }
    }
}
