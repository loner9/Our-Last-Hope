using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{
    GameObject goalLocator;
    MeshRenderer meshRenderer;
    [SerializeField] string[] keys;
    // Start is called before the first frame update
    void Start()
    {
        goalLocator = GameObject.FindGameObjectWithTag("GoalLocator");
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Inventory.Instance.CheckKeysPresent(keys))
        {
            meshRenderer.enabled = true;
            if (goalLocator != null)
            {
                // goalLocator.GetComponentInChildren
                goalLocator.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Goal : " + Inventory.Instance.CheckKeysPresent(keys));
            goalLocator.transform.GetChild(0).gameObject.SetActive(false);
            if (Inventory.Instance.CheckKeysPresent(keys))
            {
                GameManager.Instance.GameComplete();
            }
        }

    }
}
