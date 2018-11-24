using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas canvas;
    public GraphicRaycaster raycaster;

    private GameObject draggingObject;
    private RectTransform draggingPlane;

    public void OnBeginDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);

        foreach(RaycastResult result in results)
        {
            if (result.gameObject.tag == "draggable")
            {
                draggingObject = result.gameObject;
                break;
            }
        }

        draggingPlane = transform as RectTransform;
        if (draggingObject != null)
        {
            SetDraggedPosition(eventData);
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if (draggingObject != null)
        {
            SetDraggedPosition(data);
        }
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
        {
            draggingPlane = data.pointerEnter.transform as RectTransform;
        }

        var rt = draggingObject.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = draggingPlane.rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggingObject = null;
    }
}
