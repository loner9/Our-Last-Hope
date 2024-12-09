using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerWave : MonoBehaviour
{
    [SerializeField] int maxSequence; // Maximum number of sequences (set in Inspector)
    List<GameObject> spawnList = new List<GameObject>();
    GameObject[] spawnSequence;
    bool spawnActive = false;
    int currentSequence = 0;

    void Start()
    {
        // Initialize the spawn sequence
        spawnSequence = GameObject.FindGameObjectsWithTag("Spawner");

        // Populate spawnList with child objects
        foreach (Transform child in transform)
        {
            spawnList.Add(child.gameObject);
        }

        // Sort spawn list by name for consistency
        spawnList = spawnList.OrderBy(x => x.name).ToList();

        // Deactivate all spawn points initially
        foreach (GameObject item in spawnList)
        {
            item.SetActive(false);
        }
    }

    void Update()
    {
        // Initialize the first sequence if inactive
        if (!spawnActive)
        {
            Init();
        }
    }

    void Init()
    {
        if (currentSequence < maxSequence)
        {
            spawnActive = true;
            ProcessSequence();
        }
    }

    void ProcessSequence()
    {
        Debug.Log("Sequence: " + currentSequence);

        // Deactivate all spawn points
        foreach (GameObject item in spawnList)
        {
            item.SetActive(false);
        }

        // Activate the current sequence's spawn point
        if (currentSequence == maxSequence - 1 || currentSequence >= maxSequence)
        {
            Debug.Log("Final sequence! Activating all spawners.");
            // Activate all spawners
            foreach (GameObject item in spawnList)
            {
                item.SetActive(true);
            }
        }
        else if (currentSequence < spawnList.Count)
        {
            // Activate the current sequence's spawner
            GameObject spawn = spawnList[currentSequence];
            spawn.SetActive(true);
        }

        // Periodically check if the wave is cleared
        StartCoroutine(CheckZombs());
    }

    IEnumerator CheckZombs()
    {
        while (spawnActive)
        {
            yield return new WaitForSeconds(0.5f);

            // Check if all zombies are destroyed
            GameObject[] zombies = GameObject.FindGameObjectsWithTag("enemy");

            if (zombies.Length == 0)
            {
                spawnActive = false;
                currentSequence++;

                if (currentSequence == maxSequence - 1 || currentSequence >= maxSequence)
                {
                    Debug.Log("All waves completed!");
                    GameManager.Instance.GameComplete();
                }
                else if (currentSequence < spawnList.Count)
                {
                    Invoke(nameof(ProcessSequence), 3.0f); // Start the next sequence after a delay
                }

                // if (currentSequence < maxSequence)
                // {
                //     Invoke(nameof(ProcessSequence), 3.0f); // Start the next sequence after a delay
                // }
                // else
                // {
                //     Debug.Log("All waves completed!");
                // }
            }
        }
    }
}
