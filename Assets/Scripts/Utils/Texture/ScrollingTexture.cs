using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    public float scrollSpeed;
    float currentScrollSpeed;
    private Vector2 savedOffset;
    MeshRenderer currentRenderer;

    void Start()
    {
        currentRenderer = GetComponent<MeshRenderer>();
        savedOffset = currentRenderer.sharedMaterial.GetTextureOffset("_MainTex");

        currentScrollSpeed = scrollSpeed;
    }

    public void Resume()
    {
        currentScrollSpeed = scrollSpeed;
    }

    public void Stop()
    {
        currentScrollSpeed = 0;
    }

    void Update()
    {
        if (currentScrollSpeed != 0)
        {
            float y = Mathf.Repeat(Time.time * currentScrollSpeed, 1);
            Vector2 offset = new Vector2(savedOffset.x % 1, y % 1);
            currentRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
        }
    }
}
