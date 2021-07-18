using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZondType : MonoBehaviour, IPointerUpHandler, IPointerDownHandler,IPointerExitHandler
{   
    public MagazineScrollClamper MSC;
    public void OnPointerUp(PointerEventData eventData)
    {
        MSC.Follow(true);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        MSC.Follow(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MSC.Follow(true);
    }
}
