using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ResetPlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("UI_Main Menu", 1); // Mengatur level yang bisa diakses sesuai dengan nilai di Inspector
        PlayerPrefs.SetInt("MainHub Sore", 1); // Mengatur level 1 sesuai dengan nilai di Inspector
        PlayerPrefs.SetInt("HasLaunched", 1); // Menandakan bahwa game telah diluncurkan setidaknya sekali
    }
}
