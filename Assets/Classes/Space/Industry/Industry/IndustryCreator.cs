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
			recipe = (Recipe)miningRecipesArr[deposit.idResource];
			deposit.isExistConstruction = true;
		}

		industryConstruction.resourceDeposit = deposit;
		industryConstruction.stageProcess = 0;
		industryConstruction.state = EProducingState.finished;
		industryConstruction.recipe = recipe;

		return industryConstruction;
	}

	private static void CalcResourcesReserv(Storage storage, Industry industry)
	{
		List<GoodsStack> result = new List<GoodsStack>();
		for (int c = 0; c < industry.construction.Length; c++)
		{
			for (int r = 0; r < industry.construction[c].recipe.perTurn.Length; r++)
			{
				if (industry.construction[c].recipe.perTurn[r].amount < 0)
				{
					int index = result.FindIndex(x => x.id == industry.construction[c].recipe.perTurn[r].goodsId);
					if (index > -1)
					{
						result[index] = new GoodsStack
						{
							id = result[index].id,
							quantity = result[index].quantity + (int)(industry.construction[c].recipe.perTurn[r].amount * Settings.Industry.PROCESSING_RESERV_STEPS * -1)
						};
						continue;
					}
					result.Add(new GoodsStack {
						id = result[index].id,
						quantity = (int)(industry.construction[c].recipe.perTurn[r].amount * Settings.Industry.PROCESSING_RESERV_STEPS * -1)
					});
				}
			}
		}
	}
}
