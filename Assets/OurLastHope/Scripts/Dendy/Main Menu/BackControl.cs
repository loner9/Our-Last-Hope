using UnityEngine;

public class BackControl : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; // Objek yang akan dibuka/tutup, jika ada

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (targetObject != null)
            {
                bool isActive = targetObject.activeSelf;
                targetObject.SetActive(!isActive); // Toggle aktif/tidak aktif
                Debug.Log($"{targetObject.name} {(isActive ? "disembunyikan" : "dibuka")} saat tombol Esc ditekan.");
            }
            else
            {
                gameObject.SetActive(false);
                Debug.Log($"{gameObject.name} disembunyikan saat tombol Esc ditekan.");
            }
        }
    }
}
