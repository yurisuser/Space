namespace AI.AISubStar.ProdModule
{
	public static class ProducingModuleManager
	{
		private static AIBehaviour<ProdConstructionWrapper> behav = new BehaveProducingModule();

		public static void Tick(SubStarBody station, ProducingConstruction module)
		{
			behav.Tick(new ProdConstructionWrapper{body = station, module = module});
		}
	}
}
