using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectTransparency : MonoBehaviour
{
    public Transform player; // Referensi ke player
    public LayerMask obstructionLayer; // Layer untuk objek yang bisa menghalangi pandangan

    private HashSet<Renderer> previousObstructingObjects = new HashSet<Renderer>(); // Objek yang menghalangi sebelumnya
    private RaycastHit[] rayHits = new RaycastHit[10]; // Pooling untuk raycast

    void Update()
    {
        // Raycast dari kamera ke player
        Vector3 direction = player.position - transform.position;
        float distance = Vector3.Distance(player.position, transform.position);
        int hitCount = Physics.RaycastNonAlloc(transform.position, direction, rayHits, distance, obstructionLayer);

        HashSet<Renderer> currentObstructingObjects = new HashSet<Renderer>();

        for (int i = 0; i < hitCount; i++)
        {
            RaycastHit hit = rayHits[i];
            // Debug.Log("Hit by ray : " + hit.collider.name);
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                currentObstructingObjects.Add(rend);

                // Jika objek baru ditemukan, ubah ke Transparent
                if (!previousObstructingObjects.Contains(rend))
                {
                    SetSurfaceType(rend, true);
                }
            }
            else if (hit.collider.transform.childCount > 0)
            {
                foreach (Transform child in hit.collider.transform)
                {
                    Renderer childRend = child.GetComponent<Renderer>();
                    if (childRend != null)
                    {
                        currentObstructingObjects.Add(childRend);

                        // Jika objek baru ditemukan, ubah ke Transparent
                        if (!previousObstructingObjects.Contains(childRend))
                        {
                            SetSurfaceType(childRend, true);
                        }
                    }
                }
            }
        }

        // Kembalikan objek yang tidak lagi menghalangi ke Opaque
        foreach (Renderer rend in previousObstructingObjects)
        {
            if (!currentObstructingObjects.Contains(rend))
            {
                SetSurfaceType(rend, false);
            }
        }

        // Perbarui daftar objek yang menghalangi
        previousObstructingObjects = currentObstructingObjects;
    }

    void SetSurfaceType(Renderer renderer, bool isTransparent)
    {
        foreach (Material mat in renderer.materials)
        {
            if (isTransparent)
            {
                // Ubah ke Transparent
                mat.SetInt("_Surface", 1); // Transparent
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0); // Nonaktifkan depth writing
                mat.renderQueue = 3001; // Transparent queue
                mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                mat.DisableKeyword("_SURFACE_TYPE_OPAQUE");

                // Ubah alpha pada Albedo (Base Map)
                if (mat.HasProperty("_BaseColor"))
                {
                    Color baseColor = mat.GetColor("_BaseColor");
                    baseColor.a = 0.3f; // Transparansi
                    mat.SetColor("_BaseColor", baseColor);
                }
            }
            else
            {
                // Ubah ke Opaque
                mat.SetInt("_Surface", 0); // Opaque
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1); // Aktifkan depth writing
                mat.renderQueue = 2000; // Default queue
                mat.EnableKeyword("_SURFACE_TYPE_OPAQUE");
                mat.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");

                // Kembalikan alpha pada Albedo (Base Map)
                if (mat.HasProperty("_BaseColor"))
                {
                    Color baseColor = mat.GetColor("_BaseColor");
                    baseColor.a = 1f; // Solid
                    mat.SetColor("_BaseColor", baseColor);
                }
            }
        }
    }
}
