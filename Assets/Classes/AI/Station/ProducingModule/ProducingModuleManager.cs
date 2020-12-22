﻿namespace AI.AIStation.ProdModule
{
	public static class ProducingModuleManager
	{
		private static AIBehaviour<ProdModuleWrapper> behav = new BehaveProducingModule();

		public static void Tick(Station station, ProducingConstruction module)
		{
			behav.Tick(new ProdModuleWrapper{station = station, module = module});
		}
	}
}
