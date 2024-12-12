using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutObstaclesURP : MonoBehaviour
{
    public Transform player;
    public LayerMask obstructionLayer;

    private Dictionary<Renderer, Coroutine> fadingOutObjects = new Dictionary<Renderer, Coroutine>();
    private List<Renderer> objectsToRestore = new List<Renderer>();

    void Update()
    {
        foreach (Renderer rend in fadingOutObjects.Keys)
        {
            objectsToRestore.Add(rend);
        }

        Vector3 direction = player.position - transform.position;
        float distance = Vector3.Distance(player.position, transform.position);

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, obstructionLayer);

        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();

            if (rend != null)
            {
                if (fadingOutObjects.ContainsKey(rend))
                {
                    objectsToRestore.Remove(rend);
                }
                else
                {
                    Coroutine fadeCoroutine = StartCoroutine(FadeMaterial(rend, 0.3f));
                    fadingOutObjects[rend] = fadeCoroutine;
                }
            }
            else if (hit.collider.transform.childCount > 0)
            {
                Transform child = hit.collider.transform.GetChild(0);
                Renderer childRend = child.GetComponent<Renderer>();
                if (childRend != null)
                {
                    if (fadingOutObjects.ContainsKey(childRend))
                    {
                        objectsToRestore.Remove(childRend);
                    }
                    else
                    {
                        Coroutine fadeCoroutine = StartCoroutine(FadeMaterial(childRend, 0.3f));
                        fadingOutObjects[childRend] = fadeCoroutine;
                    }
                }
            }
        }

        foreach (Renderer rend in objectsToRestore)
        {
            if (fadingOutObjects.ContainsKey(rend))
            {
                StopCoroutine(fadingOutObjects[rend]);
                fadingOutObjects.Remove(rend);
            }
            StartCoroutine(FadeMaterial(rend, 1f));
        }

        objectsToRestore.Clear();
    }

    IEnumerator FadeMaterial(Renderer renderer, float targetAlpha)
    {
        foreach (Material mat in renderer.materials)
        {
            if (mat.HasProperty("_BaseColor"))
            {
                Color color = mat.GetColor("_BaseColor");
                float startAlpha = color.a;
                float duration = 0.5f;
                float elapsed = 0f;

                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;
                    float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
                    color.a = newAlpha;
                    mat.SetColor("_BaseColor", color);
                    yield return null;
                }

                color.a = targetAlpha;
                mat.SetColor("_BaseColor", color);
            }
        }
    }
}
