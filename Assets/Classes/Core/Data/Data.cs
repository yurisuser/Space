public partial struct Data
{
	//Space
	public static Star[] starsArr;
	public static Planet[] planetsArr;
	public static Moon[] moonsArr;
	public static Probability[] planetsOfStarProbabilityArr;
	public static Probability[] moonOfPlanetProbabilityArr;
	public static PlanetaryResourcesProbability[] planetaryResourcesProbabilityArr;
	public static string[] constellationNames;
	//Goods
	public static Goods[] goodsArr;
	public static ProductRecipe[] productRecipeArr;
	public static MiningRecipe[] miningRecipesArr;
	//Ships
	public static ShipRapam[] shipsParamArr;
	//other
	public static string[] greekLetters;
	public static string[] smallLetters;
	public static string[] romeIntegers;

	public static void Init()
	{
		greekLetters = new string[] { "α", "β", "γ", "δ", "ε", "ζ", "η", "θ", "ι", "κ", "λ", "μ", "ν", "ξ", "ο", "π", "ρ", "σ", "τ", "υ", "φ", "χ", "ψ", "ω",
			".α", ".β", ".γ", ".δ", ".ε", ".ζ", ".η", ".θ", ".ι", ".κ", ".λ", ".μ", ".ν", ".ξ", ".ο", ".π", ".ρ", ".σ", ".τ", ".υ", ".φ", ".χ", ".ψ", ".ω",
		};
		smallLetters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
		romeIntegers = new string[] { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX", "XX" };
		DBReader.Read();
	}
}
