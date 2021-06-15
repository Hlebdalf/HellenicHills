using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchToPlay : MonoBehaviour, IPointerClickHandler
{
    public UIButtonManager canvas;
    public void OnPointerClick(PointerEventData eventData)
    {
        canvas.StartSession();
    }
}
