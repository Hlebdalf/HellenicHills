using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagazineScrollClamper : MonoBehaviour
{
    public int steps = 5;
    public float treshold = 0.05f;
    public float speed;
    public float sensivity;
    public GameObject content;
    private Scrollbar sb;
    [SerializeField]
    private float target = 0.5f;
    private float prePos;
    private RectTransform contTrans;


    private int passer = 0;

    void Start()
    {
        steps -= 1;
        sb = GetComponent<Scrollbar>();
        contTrans = content.GetComponent<RectTransform>();
        prePos = contTrans.position.x;

    }
    /*private void Update()
    {
        contTrans.position = new Vector3((contTrans.position.x - prePos) * sensivity + prePos, contTrans.position.y, contTrans.position.z);
        prePos = contTrans.position.x; //RETURN LATER
    }*/
    public void Follow(bool isFolow)
    {
        if (isFolow)
        {
            StartCoroutine(ToTarget());
        }
        else StopCoroutine(ToTarget());
    }

    
    public void ChangeValue()
    {
        target = Mathf.Round(sb.value * steps) * 1 / (float)steps;
    }

    IEnumerator ToTarget()
    {
        while (Mathf.Abs(target - sb.value) > treshold)
        {

            if (target > sb.value) sb.value += speed;
            else sb.value -= speed;
            yield return new WaitForFixedUpdate();
        }

    }
}
