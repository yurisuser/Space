using AI.AIShip;
using AI.AIStation;

namespace AI
{
	public static class AIManager
	{
		public static void Tick()
		{
			AIShipManager.Tick();
			AIStationManager.Tick();
			Utilities.ShowMeObject(Galaxy.StarSystemsArr[11].StationArr[0].cargohold);
		}
	}
}
