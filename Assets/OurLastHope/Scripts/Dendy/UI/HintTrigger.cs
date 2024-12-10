using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    [SerializeField] private GameObject hintUI; // Assign prefab hint UI melalui Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hintUI.SetActive(true); // Aktifkan UI hint saat pemain masuk ke area trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hintUI.SetActive(false); // Nonaktifkan UI hint saat pemain keluar dari area trigger
        }
    }
}
