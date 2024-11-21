using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject[] zombiePrefabs; // Array untuk jenis-jenis zombie yang akan di-spawn
    public int zombiesToSpawn = 5;     // Jumlah zombie yang akan di-spawn
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); // Ukuran area spawner (lebar, panjang)

    private bool spawnTriggered = false; // Untuk mengecek apakah spawn kedua telah dipicu

    void Start()
    {
        // Spawn zombie di awal permainan
        SpawnZombies();
    }

    // Method untuk spawn zombie
    void SpawnZombies()
    {
        for (int i = 0; i < zombiesToSpawn; i++)
        {
            // Pilih posisi acak dalam area spawner
            Vector3 randomPosition = GetRandomPositionInArea();
            
            // Pilih jenis zombie secara acak dari array
            GameObject zombieToSpawn = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
            
            // Spawn zombie
            Instantiate(zombieToSpawn, randomPosition, Quaternion.identity);
        }
    }

    // Menghitung posisi acak di dalam area spawner
    Vector3 GetRandomPositionInArea()
    {
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomZ = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
        return transform.position + new Vector3(randomX, 0, randomZ);
    }
    
    void OnDrawGizmos()
    {
        // Mengatur warna Gizmos menjadi hijau
        Gizmos.color = Color.green;

        // Menggambar wire cube untuk mewakili area spawner
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, 1, spawnAreaSize.y));
    }
}
