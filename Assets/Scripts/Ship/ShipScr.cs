using UnityEngine;
using System.Collections;
using System;
using AI;

public class ShipScr : MonoBehaviour
{
	private Order order;
	private GameObject destOrder;
	private GameObject destStep;
	private int currentTurn;
	private float progress = 0;
	private Ship ship;
	public int shipId; //for scene
	void Start()
	{

		if (ship.id == 1018)
		{
			gameObject.GetComponent<Renderer>().material.color = Color.cyan;
		}
	}

	public void SetShip(Ship ship)
	{
		this.ship = ship;
		shipId = ship.id;
		order = ship.order.Clone();
		DrawDestinations();
	}
	void Update()
	{
		CheckEndTurn();
		Move();
		MoveNavigationPoints();
	}

	private void CheckEndTurn()
	{
		if (currentTurn != Turner.GetCurrentTime())
		{
			progress = 0;
			currentTurn = Turner.GetCurrentTime();
			order = ship.order.Clone();
		}
	}

	private void DrawDestinations()
	{
		destOrder = GameObject.Instantiate(
				Resources.Load("Prefabs/Ships/ordDest") as GameObject,
				ship.order.attribute.destinationOrder,
				Quaternion.identity);

		destStep = GameObject.Instantiate(
		Resources.Load("Prefabs/Ships/TestShipOrderDest") as GameObject,
		ship.order.attribute.destinationOrder,
		Quaternion.identity);
		destStep.transform.localScale *= 50;
	}
	private void Move()
	{
		transform.position = order.attribute.currentPosition;
		transform.position = Vector3.Lerp(order.attribute.currentPosition, order.attribute.destinationStep, progress);
		progress += Time.deltaTime / Turner.turn_length;
	}

	private void MoveNavigationPoints()
	{
		if (destOrder.transform.position != ship.order.attribute.destinationOrder)
			destOrder.transform.position = ship.order.attribute.destinationOrder;
		if (destStep.transform.position != ship.order.attribute.destinationStep)
			destStep.transform.position = ship.order.attribute.destinationStep;
	}
}
