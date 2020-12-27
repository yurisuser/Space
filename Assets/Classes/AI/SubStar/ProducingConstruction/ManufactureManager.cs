﻿namespace AI.AISubStar.Manufacture
{
	public static class ManufactureManager
	{
		private static AIBehaviour<ManufactureWrapper> behav = new BehaveManufacture();

		public static void Tick(SubStarBody station, ManufactureConstruction module)
		{
			behav.Tick(new ManufactureWrapper{body = station, module = module});
		}
	}
}