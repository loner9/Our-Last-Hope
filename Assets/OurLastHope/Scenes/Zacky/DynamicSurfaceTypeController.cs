using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSurfaceTypeController : MonoBehaviour
{
    public Transform player; // Referensi ke player
    public Renderer playerRenderer; // Renderer player agar selalu terlihat
    public LayerMask obstructionLayer; // Layer untuk objek yang bisa menghalangi pandangan

    private HashSet<Renderer> previousObstructingObjects = new HashSet<Renderer>(); // Cache objek yang sedang menghalangi
    private RaycastHit[] rayHits = new RaycastHit[10]; // Pooling untuk raycast

    void Start()
    {
        // Atur Render Queue material player ke 3002
        if (playerRenderer != null)
        {
            SetPlayerRenderQueue(playerRenderer);
        }
    }

    void Update()
    {
        List<Renderer> currentObstructingObjects = new List<Renderer>();

        // Raycast dari kamera ke player
        Vector3 direction = player.position - transform.position;
        float distance = Vector3.Distance(player.position, transform.position);
        int hitCount = Physics.RaycastNonAlloc(transform.position, direction, rayHits, distance, obstructionLayer);

        for (int i = 0; i < hitCount; i++)
        {
            RaycastHit hit = rayHits[i];
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
        }

        // Kembalikan objek yang tidak lagi menghalangi ke Opaque
        foreach (Renderer rend in previousObstructingObjects)
        {
            if (!currentObstructingObjects.Contains(rend))
            {
                SetSurfaceType(rend, false);
            }
        }

        // Perbarui daftar obstructing objects
        previousObstructingObjects.Clear();
        previousObstructingObjects.UnionWith(currentObstructingObjects);
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

    void SetPlayerRenderQueue(Renderer playerRenderer)
    {
        foreach (Material mat in playerRenderer.materials)
        {
            mat.renderQueue = 3002; // Pastikan player dirender terakhir
        }
    }
}

