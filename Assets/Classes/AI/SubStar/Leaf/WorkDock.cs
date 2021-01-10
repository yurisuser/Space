namespace AI.AISubStar
{
	public class WorkDock : Leaf<SubStarBody>
	{
		public override EStateNode Tick(SubStarBody body)
		{
			for (int i = body.controlCentre.dock.dockedShips.Count - 1; i >= 0; i--)
			{
				if (body.controlCentre.dock.dockedShips[i].state != EShipState.docked) continue;
				Docker.ToUndock(body.controlCentre.dock.dockedShips[i]);
			}
			return EStateNode.SUCCESS;
		}
	}
}
