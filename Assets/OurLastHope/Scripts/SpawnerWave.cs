using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerWave : MonoBehaviour
{
    [SerializeField] int maxSequence; // Maximum number of sequences (set in Inspector)
    List<GameObject> spawnList = new List<GameObject>();
    List<bool> spawnActiveList = new List<bool>();
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
            spawnActiveList.Add(child.gameObject.activeSelf);
        }

        // Sort spawn list by name for consistency
        spawnList = spawnList.OrderBy(x => x.name).ToList();

        // Deactivate all spawn points initially
        foreach (GameObject item in spawnList)
        {
            item.SetActive(false);
        }
    }

    void FixedUpdate()
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
        // Deactivate all spawn points
        foreach (GameObject item in spawnList)
        {
            item.SetActive(false);
        }

        // Activate the current sequence's spawn point
        // if (currentSequence == maxSequence - 1 || currentSequence >= maxSequence)
        // {
        //     Debug.Log("Final sequence! Activating all spawners.");
        //     // Activate all spawners
        //     foreach (GameObject item in spawnList)
        //     {
        //         item.SetActive(true);
        //     }
        // }
        // else

        if (spawnActiveList.All(x => x))
        {
            currentSequence = maxSequence;
        }

        for (int i = 0; i < spawnActiveList.Count; i++)
        {
            bool currentActive = spawnActiveList[i];

            if (!currentActive)
            {
                currentSequence = i;
                spawnActiveList[i] = true;
                GameObject spawn = spawnList[i];
                spawn.SetActive(true);
                break;
            }
        }
        // if (currentSequence < spawnList.Count)
        // {
        //     // Activate the current sequence's spawner
        //     GameObject spawn = spawnList[currentSequence];
        //     spawn.SetActive(true);
        // }

        // Periodically check if the wave is cleared
        StartCoroutine(CheckZombs());
    }

    IEnumerator CheckZombs()
    {
        while (spawnActive)
        {
            yield return new WaitForSeconds(2f);

            // Check if all zombies are destroyed
            GameObject[] zombies = GameObject.FindGameObjectsWithTag("enemy");
            // if (zombies.All(x => x.GetComponent<EnemyDeadHandler>().dead == true)){

            // }

            // if (zombies.Length == 0)
            if (zombies.All(x => x.GetComponent<EnemyDeadHandler>().dead == true))
            {
                // spawnActive = false;

                // if (currentSequence == maxSequence - 1 || currentSequence >= maxSequence)
                // {
                //     Debug.Log("All waves completed!");
                //     GameManager.Instance.GameComplete();
                // }
                // else if (currentSequence < spawnList.Count)
                // {
                //     Invoke(nameof(ProcessSequence), 3.0f); // Start the next sequence after a delay
                // }

                if (currentSequence < maxSequence)
                {
                    Debug.Log("Wave " + (currentSequence + 1) + " cleared!");
                    Invoke(nameof(ProcessSequence), 1.0f); // Start the next sequence after a delay
                }
                else
                {
                    GameManager.Instance.GameComplete();
                    Debug.Log("All waves completed!");
                }
            }
        }
    }
}
