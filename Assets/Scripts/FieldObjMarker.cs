using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldObjMarker : MonoBehaviour
{
    
    public GameObject refMarker;
    private GameObject _canvas;
    private GameObject _selfMarker;
    private GameObject _ball;
    private Color _color;
    public void StartGame(string type)
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        switch (type)
        {
          case "Chargers":
              _color = Color.cyan;
              break;
          case "Missions":
              _color = Color.yellow;
              break;
          case "Parts":
              _color = Color.white;
              break;
          default:
              _color = Color.red;
              break;
        }
        
        _selfMarker = Instantiate(refMarker);
        _selfMarker.GetComponent<Transform>().SetParent(_canvas.transform.GetChild(2).transform);
        _selfMarker.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position);
        StartCoroutine(MarkerPosition());
    }

    private IEnumerator MarkerPosition()
    {
        while (gameObject.GetComponent<Transform>().position.x > _ball.GetComponent<Transform>().position.x + 40)
        {
            _selfMarker.GetComponent<Image>().color = new Color(_color.r, _color.g, _color.b, 1 - (gameObject.GetComponent<Transform>().position.x - _ball.GetComponent<Transform>().position.x) / 1200);
            RectTransform nowTransform = _selfMarker.GetComponent<RectTransform>();
            nowTransform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x, Screen.height - 120, 1);
            _selfMarker.GetComponent<RectTransform>().position = nowTransform.position;
            yield return new WaitForEndOfFrame();
        }
        Destroy(_selfMarker);
    }
}
