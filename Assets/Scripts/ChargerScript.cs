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
        while (gameObject.GetComponent<Transform>().position.x > ball.GetComponent<Transform>().position.x + 40)
        {
            selfMarker.GetComponent<Image>().color = new Color(0,1,1,1-(gameObject.GetComponent<Transform>().position.x - ball.GetComponent<Transform>().position.x) / 800);
            RectTransform nowTransfporm = selfMarker.GetComponent<RectTransform>();
            nowTransfporm.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x, Screen.height - 120, 1);
            selfMarker.GetComponent<RectTransform>().position = nowTransfporm.position;
            yield return new WaitForEndOfFrame();
        }
        Destroy(selfMarker);
        yield break;
    }
}
