using System.Collections.Generic;
using UnityEngine;

public class PreviewerBuilderScr : MonoBehaviour
{
	private List<ResourceDeposit> resources;
	private List<List<int>> values;

	public void CreatePreview(SubStarBody planet, Transform parent)
	{
		Calculate(planet);
		Draw(parent.Find("res"));
	}

	public int GetCountResources()
	{
		return resources.Count;
	}

	private void Calculate(SubStarBody planet)
	{
		resources = new List<ResourceDeposit>();
		values = new List<List<int>>();
		List<int> temp = new List<int>();
		for (int i = 0; i < planet.industry.industrialPointsArr.Length; i++)
		{
			if (!resources.Exists(x => x.idResource == planet.industry.industrialPointsArr[i].resourceDeposit.idResource))
			{
				resources.Add(planet.industry.industrialPointsArr[i].resourceDeposit);
				values.Add(new List<int> { planet.industry.industrialPointsArr[i].resourceDeposit.extraction });
				continue;
			}
			int index = resources.FindIndex(x => x.idResource == planet.industry.industrialPointsArr[i].resourceDeposit.idResource);
			values[index].Add(planet.industry.industrialPointsArr[i].resourceDeposit.extraction);
		}
		foreach (var item in values)
		{
			item.Sort((x, y) => y - x);
		}
	}

	private void Draw(Transform parent)
	{
		float margin = 4f;
		float heightPrefab = PrefabService.UI.PlanetResourcePreview.GetComponent<RectTransform>().rect.height + margin;
		for (int i = 0; i < resources.Count; i++)
		{
			GameObject resourcePreview = Instantiate(
				PrefabService.UI.PlanetResourcePreview,
				new Vector3(
					5,
					parent.GetComponent<RectTransform>().rect.height - heightPrefab * i - heightPrefab - margin - 20 ,
					0
					),
				Quaternion.identity,
				parent
				);
			resourcePreview.name += i.ToString();
			resourcePreview.GetComponent<PreviewPlanetResourceScr>().SetParam(resources[i].idResource, values[i].ToArray());
		}
	}
}
