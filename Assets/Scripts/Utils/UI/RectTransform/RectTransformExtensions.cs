using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RectTransformExtensions
{
    public static Rect ToScreenSpace(this RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }
}