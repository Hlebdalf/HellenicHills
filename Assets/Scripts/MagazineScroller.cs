using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagazineScroller : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public float sens;
    private RectTransform rt;
    private float nowx = 0, prex = 0, deltax = 0;
    private int childCNT;
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        childCNT = transform.GetChildCount();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        nowx = eventData.pointerCurrentRaycast.screenPosition.x;
        prex = nowx; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        nowx = eventData.pointerCurrentRaycast.screenPosition.x;
        deltax = (nowx - prex)*sens;
        rt.position = new Vector3(rt.position.x + deltax, rt.position.y, rt.position.z);
        prex = nowx;
        if (rt.position.x > 0)
        {
            rt.position = new Vector3(0, rt.position.y, rt.position.z);
        }
        else if (rt.position.x < -(childCNT - 1) * Screen.width)
        {
            rt.position =  new Vector3(-(childCNT - 1) * Screen.width, rt.position.y, rt.position.z);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        prex = 0;
        nowx = 0;
        deltax = 0;
        StartCoroutine(Rounder());
    }

    IEnumerator Rounder()
    {
        float target = Targeter();
        while(Mathf.Abs(rt.position.x - target) > 2)
        {
            rt.position = new Vector3(rt.position.x - (rt.position.x - target)/2, rt.position.y, rt.position.z);
            yield return new WaitForFixedUpdate();
        }
        
    }

    private float Targeter()
    {
        float target = Mathf.Round(rt.position.x / Screen.width) * Screen.width;
        return target;
    }
}
