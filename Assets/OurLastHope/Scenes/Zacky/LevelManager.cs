using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{   //Cek dulu di unity editor apakah scene sudah diadd di build setting

    //public variable untuk nama scene
    public string sceneName;

    //tombol untuk ganti scene (contoh saja untuk debug)
    public KeyCode loadSceneKey = KeyCode.L;

    void Update()
    {
        //cek apakah tombol sudah ditekan
        if (Input.GetKeyDown(loadSceneKey))
        {
            //cek apakah sceneName tidak kosong
            if (!string.IsNullOrEmpty(sceneName))
            {
                Debug.Log("Masuk ke=" + sceneName);
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError("tidak ada nama scene");
            }
        }
    }
}
