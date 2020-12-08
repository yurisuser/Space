using UnityEngine;
using System.Collections;
using System;
using AI;

public class ShipScr : MonoBehaviour
{
	private GameObject destOrder;
	private GameObject destStep;
	private int oldTurn;
	private Vector3 oldDestinationStep;
	private float progress;
	public Ship ship;
	public string id;
	void Start()
	{
		DrawDestinations();
		if (ship.Id == 1018)
		{
			gameObject.GetComponent<Renderer>().material.color = Color.cyan;
		}
	}

	public void SetShip(Ship ship)
	{
		this.ship = ship;
		this.id = ship.Id.ToString();
	}
	void Update()
	{
		Move();
		MoveNavigationPoints();
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
		transform.position = ship.order.attribute.currentPosition;
		transform.position = Vector3.Lerp(ship.order.attribute.currentPosition, ship.order.attribute.destinationStep, progress);
		progress += Time.deltaTime / Settings.Time.TURN_LENGTH;

		if (oldTurn != Turner.GetCurrentTime()) progress = 0;
		oldTurn = Turner.GetCurrentTime();
		oldDestinationStep = ship.order.attribute.destinationStep;
		//if (ship.Id == 1097) Debug.Log("Progress " + progress);
	}

	private void MoveNavigationPoints()
	{
		if (destOrder.transform.position != ship.order.attribute.destinationOrder)
			destOrder.transform.position = ship.order.attribute.destinationOrder;
		if (destStep.transform.position != ship.order.attribute.destinationStep)
			destStep.transform.position = ship.order.attribute.destinationStep;
	}
}
