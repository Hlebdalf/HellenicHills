using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BallControlLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public BallMovement BallClass;
	public void OnPointerDown(PointerEventData eventData)
	{
		BallClass.StartMoveLeft();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		BallClass.EndMoveLeft();
	}
}
