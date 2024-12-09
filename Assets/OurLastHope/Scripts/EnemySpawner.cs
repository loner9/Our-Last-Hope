using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject[] zombiePrefabs; // Array untuk jenis-jenis zombie yang akan di-spawn
    public float[] zombieWeight; // Array untuk berat zombie
    private Dictionary<GameObject, float> zombieWeights = new Dictionary<GameObject, float>();
    public int zombiesToSpawn = 5;     // Jumlah zombie yang akan di-spawn
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); // Ukuran area spawner (lebar, panjang)

    private bool spawnTriggered = false; // Untuk mengecek apakah spawn kedua telah dipicu

    void Awake(){
        for (int i = 0; i < zombiePrefabs.Length; i++){
            zombieWeights.Add(zombiePrefabs[i], zombieWeight[i]);
        }
    }

    void Start()
    {
        
        // Spawn zombie di awal permainan
        
    }

    // Method untuk spawn zombie
    void SpawnZombies()
    {
        for (int i = 0; i < zombiesToSpawn; i++)
        {
            // Pilih posisi acak dalam area spawner
            Vector3 randomPosition = GetRandomPositionInArea();
            
            // Pilih jenis zombie secara acak dari array
            GameObject zomb = GetWeightedRandomValue(zombieWeights);
            // GameObject zombieToSpawn = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
            
            
            // Spawn zombie
            Instantiate(zomb, randomPosition, Quaternion.identity);
        }
    }

    private static T GetWeightedRandomValue<T>(Dictionary<T, float> options)
    {
        if (options == null || options.Count == 0)
        {
            throw new ArgumentNullException(nameof(options), "Options dictionary cannot be null or empty.");
        }

        var totalWeight = options.Values.Sum();
        var randomValue = Random.value * totalWeight;

        foreach (var option in options)
        {
            randomValue -= option.Value;
            if (randomValue <= 0)
            {
                return option.Key;
            }
        }

        // Should not reach here, but throw an exception just in case
        throw new InvalidOperationException("Failed to select a random value from options.");
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

    void OnEnable(){
        SpawnZombies();
    }
}
