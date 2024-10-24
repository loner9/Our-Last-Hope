using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{   // Pastikan scene ini sudah dimasukkan dalam Build Settings di Unity
    public string[] levelNames = { "Level 1", "Level 2", "Level 3", "Level 4", "Level 5" };

    // Simpan progres terakhir di PlayerPrefs dengan key ini
    private string progressKey = "LastCompletedLevel";

    private void Start()
    {
        // Bisa digunakan untuk melakukan inisialisasi jika dibutuhkan
    }

    // Fungsi untuk memuat level berdasarkan index level
    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < levelNames.Length)
        {
            // Pastikan levelIndex valid sebelum memuat level
            string levelName = levelNames[levelIndex];
            Debug.Log("Loading level: " + levelName);
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogError("Level index out of bounds!");
        }
    }

    // Fungsi untuk menyimpan progres pemain (level yang sudah selesai)
    public void CompleteLevel(int levelIndex)
    {
        if (levelIndex < levelNames.Length)
        {
            // Simpan level yang sudah diselesaikan ke PlayerPrefs
            PlayerPrefs.SetInt(progressKey, levelIndex);
            PlayerPrefs.Save();
            Debug.Log("Level " + levelNames[levelIndex] + " completed!");
        }
    }

    // Fungsi untuk mendapatkan level terakhir yang diselesaikan
    public int GetLastCompletedLevel()
    {
        // Default adalah level 0 jika belum ada progres
        return PlayerPrefs.GetInt(progressKey, 0);
    }

    // Fungsi untuk membuka level selanjutnya setelah menyelesaikan level
    public void LoadNextLevel()
    {
        int lastCompletedLevel = GetLastCompletedLevel();
        int nextLevelIndex = lastCompletedLevel + 1;

        // Cek apakah ada level selanjutnya
        if (nextLevelIndex < levelNames.Length)
        {
            // Load level berikutnya
            LoadLevel(nextLevelIndex);
        }
        else
        {
            Debug.Log("Semua level sudah diselesaikan! Tidak ada level selanjutnya.");
        }
    }
}
