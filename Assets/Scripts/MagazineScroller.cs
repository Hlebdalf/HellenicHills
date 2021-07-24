using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagazineScroller : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rt;
    private float nowx = 0, prex = 0, deltax = 0;
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        nowx = eventData.pointerCurrentRaycast.screenPosition.x;
        prex = nowx;
    }

    public void OnDrag(PointerEventData eventData)
    {
        nowx = eventData.pointerCurrentRaycast.screenPosition.x;
        deltax = nowx - prex;
        rt.position = new Vector3(rt.position.x+deltax, rt.position.y, rt.position.z);
        prex = nowx;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        prex = 0;
        nowx = 0;
        deltax = 0;
    }
}
