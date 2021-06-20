using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargerScript : MonoBehaviour
{
    public Death death;
    public GameObject refMarker;
    public GameObject canvas;
    private GameObject selfMarker;
    public GameObject ball;
    public void StartGame()
    {
        selfMarker = Instantiate(refMarker);
        selfMarker.GetComponent<Transform>().SetParent(canvas.GetComponent<Transform>());
        selfMarker.GetComponent<RectTransform>().position= Camera.main.WorldToScreenPoint(transform.position);
        StartCoroutine(MarkerPosition());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Machine_ball")
        {
            death.StartCharge();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    IEnumerator MarkerPosition()
    {
        while (gameObject.GetComponent<Transform>().position.x > ball.GetComponent<Transform>().position.x)
        {
            selfMarker.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position);
            yield return new WaitForEndOfFrame();
        }
        Destroy(selfMarker);
        yield break;
    }
}
