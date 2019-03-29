using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CarouselView : MonoBehaviour
{
    [SerializeField]
    RectTransform[] introImages;

    float scrollZoneYMin, scrollZoneYMax;
    float wide, mousePositionStartX, mousePositionEndX, dragAmount;
    float screenPosition, lastScreenPosition;
    float lerpTimer, lerpPage;

    [SerializeField]
    int pageCount = 1;
    [SerializeField]
    string side = "";

    [SerializeField]
    int swipeThrustHold = 30;
    [SerializeField]
    int spaceBetweenProfileImages = 30;
    bool canSwipe;

    [SerializeField]
    GameObject cartoonWindow;

    int currentIndex = 0;

    [SerializeField]
    GameEventInt changedIndex;

    void OnEnable()
    {
        if (changedIndex != null)
            changedIndex.Raise(currentIndex);
    }

    void Start()
    {
        scrollZoneYMin = 0;
        scrollZoneYMax = Screen.height;

        wide = cartoonWindow.GetComponent<RectTransform>().rect.width;

        for (int i = 1; i < introImages.Length; i++)
            introImages[i].anchoredPosition = new Vector2(((wide + spaceBetweenProfileImages) * i), 0);

        side = "left";

        currentIndex = 0;
    }

    void Update()
    {
        lerpTimer = lerpTimer + Time.deltaTime;
        if (lerpTimer < .333)
        {
            screenPosition = Mathf.Lerp(lastScreenPosition, lerpPage * -1, lerpTimer * 3);
            lastScreenPosition = screenPosition;
        }

        Touch? touch = null;
		if (Input.touchCount > 0)
            touch = Input.GetTouch(0);

        Vector3 touchPosition = touch.HasValue ? new Vector3(
            touch.Value.position.x,
            touch.Value.position.y,
            0) : Input.mousePosition;

        if ((Input.GetMouseButtonDown(0) || (touch.HasValue && touch.Value.phase == TouchPhase.Began))
            && touchPosition.y > scrollZoneYMin && touchPosition.y < scrollZoneYMax)
        {
            canSwipe = true;
            mousePositionStartX = touchPosition.x;
        }


        if (Input.GetMouseButton(0) || (touch.HasValue && touch.Value.phase == TouchPhase.Moved))
        {
            if (canSwipe)
            {
                mousePositionEndX = touchPosition.x;
                dragAmount = mousePositionEndX - mousePositionStartX;
                screenPosition = lastScreenPosition + dragAmount;
            }
        }

        if (Mathf.Abs(dragAmount) > swipeThrustHold && canSwipe)
        {
            canSwipe = false;
            lastScreenPosition = screenPosition;
            if (pageCount < introImages.Length)
                OnSwipeComplete();
            else if (pageCount == introImages.Length && dragAmount < 0)
                lerpTimer = 0;
            else if (pageCount == introImages.Length && dragAmount > 0)
                OnSwipeComplete();
        }

        if (Input.GetMouseButtonUp(0) || (touch.HasValue && touch.Value.phase == TouchPhase.Ended))
        {
            if (Mathf.Abs(dragAmount) < swipeThrustHold)
                lerpTimer = 0;
        }

        for (int i = 0; i < introImages.Length; i++)
        {

            introImages[i].anchoredPosition = new Vector2(screenPosition + ((wide + spaceBetweenProfileImages) * i), 0);

            if (side == "right")
            {
                float value = i == pageCount - 1 ? 1.4f : 0.7f;
                introImages[i].localScale = Vector3.Lerp(
                    introImages[i].localScale,
                    new Vector3(value, value, value),
                    Time.deltaTime * 5);
            }
            else
            {
                float value = i == pageCount ? 1.4f : 0.7f;
                introImages[i].localScale = Vector3.Lerp(
                    introImages[i].localScale,
                    new Vector3(value, value, value),
                    Time.deltaTime * 5);
            }
        }
    }

    void OnSwipeComplete()
    {
        lastScreenPosition = screenPosition;

        if (dragAmount > 0)
        {

            if (Mathf.Abs(dragAmount) > (swipeThrustHold))
            {

                if (pageCount == 0)
                {
                    lerpTimer = 0;
                    lerpPage = 0;
                }
                else
                {
                    if (side == "right")
                        pageCount--;
                    side = "left";
                    pageCount -= 1;
                    lerpTimer = 0;
                    if (pageCount < 0)
                        pageCount = 0;
                    lerpPage = (wide + spaceBetweenProfileImages) * pageCount;
                }

            }
            else
                lerpTimer = 0;

        }
        else if (dragAmount < 0)
        {

            if (Mathf.Abs(dragAmount) > (swipeThrustHold))
            {

                if (pageCount == introImages.Length)
                {
                    lerpTimer = 0;
                    lerpPage = (wide + spaceBetweenProfileImages) * introImages.Length - 1;
                }
                else
                {
                    if (side == "left")
                        pageCount++;
                    side = "right";
                    lerpTimer = 0;
                    lerpPage = (wide + spaceBetweenProfileImages) * pageCount;
                    pageCount++;
                }

            }
            else
                lerpTimer = 0;
        }

        currentIndex = pageCount - (side == "right" ? 1 : 0);
        if (changedIndex != null)
            changedIndex.Raise(currentIndex);
    }
}
