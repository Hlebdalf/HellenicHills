using UnityEngine;
using UnityEngine.EventSystems;

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
