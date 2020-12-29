using System.Collections.Generic;
using UnityEngine;
using static Data;

public static class IndustryCreator
{
	public static Industry TestCreateFullIndustry(SubStarBody body)
	{
		Industry industry = new Industry(body, null);
		List<IndustryConstruction> constructions = new List<IndustryConstruction>();
		if (body.resourcer == null)
		{
			industry.construction = new IndustryConstruction[] { GetIndustryConstruction(null) };
			return industry;
		}
		for (int i = 0; i < body.resourcer.resourceDeposits.Length; i++)
		{
			constructions.Add(GetIndustryConstruction(body.resourcer.resourceDeposits[i]));
		}
		return industry;
	}

	private static IndustryConstruction GetIndustryConstruction(ResourceDeposit deposit)
	{
		Recipe recipe = deposit == null ?
			(Recipe)productRecipeArr[Random.Range(0, productRecipeArr.Length - 1)] :
			(Recipe)miningRecipesArr[Random.Range(0, miningRecipesArr.Length - 1)];

		IndustryConstruction industryConstruction = new IndustryConstruction()
		{
			resourceDeposit = deposit,
			stageProcess = 0,
			state = EProducingState.finished,
			recipe = recipe
		};

		return industryConstruction;
	}
}
