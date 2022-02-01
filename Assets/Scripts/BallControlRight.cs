using UnityEngine;
using UnityEngine.EventSystems;

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
