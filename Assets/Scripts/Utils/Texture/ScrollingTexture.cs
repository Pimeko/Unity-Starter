using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    [SerializeField]
    Vector2 scrollSpeed;

    MeshRenderer currentRenderer;
    Vector2 currentOffset;
    bool isScrolling;

    void Start()
    {
        currentRenderer = GetComponent<MeshRenderer>();
        isScrolling = true;
        currentOffset = currentRenderer.sharedMaterial.GetTextureOffset("_MainTex");
    }

    public void Resume()
    {
        isScrolling = true;
    }

    public void Stop()
    {
        isScrolling = false;
    }

    void Update()
    {
        if (isScrolling)
        {
            currentOffset = new Vector2(
                (scrollSpeed.x * Time.time) % 1, 
                (scrollSpeed.y * Time.time) % 1);

            currentRenderer.sharedMaterial.SetTextureOffset("_MainTex", currentOffset);
        }
    }
}
