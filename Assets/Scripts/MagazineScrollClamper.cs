using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagazineScrollClamper : MonoBehaviour
{
    public int steps = 5;
    public float treshold = 0.05f;
    public float speed;
    private Scrollbar sb;
    [SerializeField]
    private float target = 0.5f;
    [SerializeField]
    private float val;

    void Start()
    {
        steps -= 1;
        sb = GetComponent<Scrollbar>();
        

    }

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
            yield return new WaitForSeconds(1 / 60);
        }
        val = sb.value;
    }
}
