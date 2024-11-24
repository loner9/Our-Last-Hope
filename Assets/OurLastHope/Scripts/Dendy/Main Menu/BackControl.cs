using UnityEngine;

public class BackControl : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            Debug.Log($"{gameObject.name} disembunyikan saat tombol Esc ditekan.");
        }
    }
}
