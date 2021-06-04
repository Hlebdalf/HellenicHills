using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BallControlRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public BallMovement BallClass;
	public void OnPointerDown(PointerEventData eventData)
	{
		BallClass.StartMoveRight();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		BallClass.EndMoveRight();
	}
}
