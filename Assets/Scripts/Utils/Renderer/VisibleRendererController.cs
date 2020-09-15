using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleRendererController : MonoBehaviour
{
    [SerializeField]
    CameraVariable currentCamera;

    /// Is Visible
    public Action<bool> onVisibilityChange;

    bool isVisible;
    public bool IsVisible => isVisible;

    void Start()
    {
        UpdateIsVisible();
    }

    void UpdateIsVisible()
    {
        Vector3 viewPos = currentCamera.Value.WorldToViewportPoint(transform.position);
        var previous = isVisible;
        isVisible = viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0;
        if (previous != isVisible)
            onVisibilityChange?.Invoke(isVisible);
    }

    void Update()
    {
        UpdateIsVisible();
    }
}