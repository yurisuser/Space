public partial struct Data
{
	//Space
	public static Star[] starsArr;
	public static Planet[] planetsArr;
	public static Moon[] moonsArr;
	public static Probability[] planetsOfStarProbabilityArr;
	public static Probability[] moonOfPlanetProbabilityArr;
	public static PlanetaryResourcesProbability[] planetaryResourcesProbabilityArr;
	//Goods
	public static Goods[] goodsArr;
	public static ProductRecipe[] productRecipeArr;
	public static MiningRecipe[] miningRecipesArr;
	//Ships
	public static ShipRapam[] shipsParamArr;

	public static void Init()
	{
		DBReader.Read();
	}
}
