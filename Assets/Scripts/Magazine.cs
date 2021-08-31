using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Magazine : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{   
    
    public GameObject parts;
    public GameObject balls;
    public GameObject ups;
    public float sens;
    private RectTransform rt;
    private List<GameObject> buttons = new List<GameObject>();
    private float nowx = 0, prex = 0, deltax = 0;
    private int childCNT;
    public static int modelType;
    private void Awake()
    {
        modelType = PlayerPrefs.GetInt("modelType");
    }
    private void Start()
    {   
        rt = gameObject.GetComponent<RectTransform>();
        rt.transform.position = new Vector3(-modelType*1080 , rt.position.y, rt.position.z);
        childCNT = transform.childCount;
        for (int i = 0; i < childCNT; i++){
            buttons.Add(transform.GetChild(i).GetChild(0).gameObject);
            buttons[i].GetComponent<BuyButton>().Refresh();
        }
    }
    public void RefreshButtons(){
        for (int i = 0; i < childCNT; i++){
            buttons[i].GetComponent<BuyButton>().Refresh();
        }
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
        ups.transform.localPosition = new Vector3((rt.position.x / 1080) * 5, 0, -(rt.position.x / 1080) * 5);
        balls.transform.localPosition = new Vector3((rt.position.x / 1080) * 5, 0, -(rt.position.x / 1080) * 5);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        prex = 0;
        nowx = 0;
        deltax = 0;
        StartCoroutine(Rounder());
    }
    public void SaveModelType(string type)
    {
        modelType = int.Parse(type);

        PlayerPrefs.SetInt("modelType",modelType);
        PlayerPrefs.Save();
    }
    IEnumerator Rounder()
    {
        float target = Targeter();
        while(Mathf.Abs(rt.position.x - target) > 2)
        {
            rt.position = new Vector3(rt.position.x - (rt.position.x - target)/2, rt.position.y, rt.position.z);
            balls.transform.localPosition = new Vector3((rt.position.x / 1080) * 5, 0, -(rt.position.x / 1080) * 5);
            ups.transform.localPosition = new Vector3((rt.position.x / 1080) * 5, 0, -(rt.position.x / 1080) * 5);
            yield return new WaitForFixedUpdate();
        }
    }

    private float Targeter()
    {
        float target = Mathf.Round(rt.position.x / Screen.width) * Screen.width;
        return target;
    }

    public void ModelSwitcher()
    {
        for (int i =0; i < childCNT; i++)
        {
            if (i == modelType) continue;
            ups.transform.GetChild(i).gameObject.SetActive(false);
            balls.transform.GetChild(i).gameObject.SetActive(false);
        }
        ups.transform.localPosition = new Vector3(-modelType * 5, 0, modelType * 5);
        balls.transform.localPosition = new Vector3(-modelType * 5, 0, modelType * 5);
    }

    public void ChoiseActive()
    {
        for (int i = 0; i < childCNT; i++)
        {
            ups.transform.GetChild(i).gameObject.SetActive(true);
            balls.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

}
