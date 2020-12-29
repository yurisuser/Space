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
			industry.construction = new IndustryConstruction[] { CreateIndustryConstruction(null) };
			return industry;
		}
		for (int i = 0; i < body.resourcer.resourceDeposits.Length; i++)
		{
			constructions.Add(CreateIndustryConstruction(body.resourcer.resourceDeposits[i]));
		}
		industry.construction = constructions.ToArray();
		return industry;
	}

	private static IndustryConstruction CreateIndustryConstruction(ResourceDeposit deposit)
	{
		IndustryConstruction industryConstruction = new IndustryConstruction();
		Recipe recipe;
		if (deposit == null)
		{
			recipe = (Recipe)productRecipeArr[Random.Range(0, productRecipeArr.Length - 1)];
		}
		else
		{
			recipe = (Recipe)miningRecipesArr[Random.Range(0, miningRecipesArr.Length - 1)];
			deposit.isExistConstruction = true;
		}

		industryConstruction.resourceDeposit = deposit;
		industryConstruction.stageProcess = 0;
		industryConstruction.state = EProducingState.finished;
		industryConstruction.recipe = recipe;

		return industryConstruction;
	}
}
