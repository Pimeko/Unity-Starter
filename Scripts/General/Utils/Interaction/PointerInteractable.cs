using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerInteractable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	UnityEvent onPointerDown;
	[SerializeField]
	UnityEvent onPointerUp;

    public void OnPointerDown(PointerEventData eventData)
    {
		if (onPointerDown != null)
			onPointerDown.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
		if (onPointerUp != null)
			onPointerUp.Invoke();
    }
}
