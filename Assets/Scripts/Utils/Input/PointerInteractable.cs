using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerInteractable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
    IPointerExitHandler, IPointerEnterHandler 
{
    [SerializeField]
    UnityEvent onPointerDown, onPointerUp;

    bool isInteracting;

    void OnEnable()
    {
        isInteracting = false;        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown?.Invoke();
        
        isInteracting = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteracting)
            return;

        onPointerDown?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerUp?.Invoke();
        isInteracting = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUp?.Invoke();
        isInteracting = false;
    }
}
